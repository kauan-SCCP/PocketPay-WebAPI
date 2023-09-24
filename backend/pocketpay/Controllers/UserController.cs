using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly IAccountRepository accountRepository;

    public UserController(IUserRepository userRepository, IAccountRepository accountRepository)
    {
        this.userRepository = userRepository;
        this.accountRepository = accountRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest data)
    {
        if (data.email == null || data.password == null)
        {
            return BadRequest();
        }

        var account = await accountRepository.GetByEmail(data.email);

        if (account == null || !BCrypt.Net.BCrypt.Verify(data.password, account.Password))
        {
            return Forbid();

        }
        
        var responseBody = new AuthResponse();
        responseBody.access_token = AuthenticationService.createToken(account);
        return Ok(responseBody);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest data)
    {
        if (data.email == null || data.password == null || data.name == null || data.surname == null || data.cpf == null)
        {
            return BadRequest();
        }

        if (userRepository.GetByEmail(data.email) != null | userRepository.GetByCPF(data.cpf) != null)
        {
            return BadRequest();
        }
        
        var user = await userRepository.Create(data.email, data.password, data.name, data.surname, data.cpf);

        var responseBody = new AuthResponse();
        responseBody.access_token = AuthenticationService.createToken(user.Account);
        return Ok(responseBody);
    }

    
    [HttpGet("account")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var email = User.Identity?.Name;
        if (email == null) {return Unauthorized();}

        var user = await userRepository.GetByEmail(email);
        if (user == null || user.Account == null) {return Unauthorized();}

        var responseBody = new UserProfileResponse();
        
        responseBody.email = user.Account.Email;
        responseBody.name = user.Name;
        responseBody.surname = user.Surname;
        responseBody.cpf = user.CPF;
        
        return Ok(responseBody);
    }

}
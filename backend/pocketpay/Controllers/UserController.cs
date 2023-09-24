using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest data)
    {
        if (data.email == null || data.password == null)
        {
            return BadRequest();
        }

        var account = await userRepository.checkCredentials(data.email, data.password);

        if (account == null)
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
        var user = await userRepository.createUser(data);

        if (user == null || user.Account == null)
        {
            return BadRequest();
        }

        var responseBody = new AuthResponse();
        responseBody.access_token = AuthenticationService.createToken(user.Account);
        return Ok(responseBody);
    }
}
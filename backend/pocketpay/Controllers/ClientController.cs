using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/client")]
public class ClientController : ControllerBase
{
    private readonly IClientRepository clientRepository;
    private readonly IAccountRepository accountRepository;

    public ClientController(IClientRepository clientRepository, IAccountRepository accountRepository)
    {
        this.clientRepository = clientRepository;
        this.accountRepository = accountRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(ClientLoginRequest data)
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
    public async Task<IActionResult> Register(ClientRegisterRequest data)
    {
        if (data.email == null || data.password == null || data.name == null || data.surname == null || data.cpf == null)
        {
            return BadRequest();
        }

        if (await clientRepository.GetByEmail(data.email) != null | await clientRepository.GetByCPF(data.cpf) != null)
        {
            return BadRequest();
        }
        
        await clientRepository.Create(data.email, data.password, data.name, data.surname, data.cpf);
        var user = await clientRepository.GetByEmail(data.email);

        var responseBody = new AuthResponse();
        responseBody.access_token = AuthenticationService.createToken(user.Account);
        return Ok(responseBody);
    }
}
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
[Route("api/v1/client")]
public class ClientController : ControllerBase
{
    private readonly IClientRepository clientRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IWalletRepository walletRepository;

    public ClientController(IClientRepository clientRepository, IAccountRepository accountRepository, IWalletRepository walletRepository)
    {
        this.clientRepository = clientRepository;
        this.accountRepository = accountRepository;
        this.walletRepository = walletRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(ClientLoginRequest data)
    {
        if (data.email == null || data.password == null)
        {
            return BadRequest();
        }
        

        var account = await accountRepository.FindByEmail(data.email);

        if (account == null || !BCrypt.Net.BCrypt.Verify(data.password, account.Password))
        {
            return Forbid();

        }

        var responseBody = new AuthResponse
        {
            access_token = AuthenticationService.createToken(account)
        };
        
        return Ok(responseBody);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(ClientRegisterRequest data)
    {
        if (data.email == null || data.password == null || data.name == null || data.surname == null || data.cpf == null)
        {
            return BadRequest();
        }

        if (await accountRepository.FindByEmail(data.email) != null || await clientRepository.FindByCPF(data.cpf) != null) 
        {
            return BadRequest();
        }

        var newAccount = await accountRepository.Create(data.email, data.password, AccountRole.Client);
        var newClient = await clientRepository.Create(newAccount, data.name, data.surname, data.cpf);
        
        await walletRepository.Create(newAccount);
        
        var responseBody = new ClientRegisterResponse()
        {
            name = newClient.Name,
            surname = newClient.Surname,
            email = newAccount.Email,
            cpf = newClient.CPF
        };

        return Created("/login", responseBody);
    }
}
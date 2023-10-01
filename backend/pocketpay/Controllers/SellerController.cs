using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/seller")]
public class SellerController : ControllerBase
{
    private readonly ISellerRepository sellerRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IWalletRepository walletRepository;

    public SellerController(ISellerRepository sellerRepository, IAccountRepository accountRepository, IWalletRepository walletRepository)
    {
        this.sellerRepository = sellerRepository;
        this.accountRepository = accountRepository;
        this.walletRepository = walletRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(SellerLoginRequest data)
    {
        if (data.email == null || data.password == null)
        {
            return BadRequest();
        }

        var account = await accountRepository.FindByEmail(data.email);

        if (account == null || !BCrypt.Net.BCrypt.Verify(data.password, account.Password)) 
        {
            return Unauthorized();
        }

        var responseBody = new AuthResponse
        {
            access_token = AuthenticationService.createToken(account)
        };

        return Ok(responseBody);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(SellerRegisterRequest data)
    {
        if (data.email == null || data.password == null || data.name == null || data.surname == null || data.cnpj == null)
        {
            return BadRequest();
        }

        if (await accountRepository.FindByEmail(data.email) != null || await sellerRepository.FindByCNPJ(data.cnpj) != null)
        {
            return BadRequest();
        }

        var newAccount = await accountRepository.Create(data.email, data.password, AccountRole.Seller);
        var newSeller = await sellerRepository.Create(newAccount, data.name, data.surname, data.cnpj);

        await walletRepository.Create(newAccount);

        var responseBody = new SellerRegisterResponse()
        {
            name = newSeller.Name,
            surname = newSeller.Surname,
            email = newAccount.Email,
            cnpj = newSeller.CNPJ
        };

        return Created("/login", responseBody);

    }
}


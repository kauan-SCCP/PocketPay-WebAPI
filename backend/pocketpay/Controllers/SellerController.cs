
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/seller")]
public class SellerController : ControllerBase
{
    private readonly ISellerRepository sellerRepository;
    private readonly IAccountRepository accountRepository;

    public SellerController(ISellerRepository sellerRepository, IAccountRepository accountRepository)
    {
        this.sellerRepository = sellerRepository;
        this.accountRepository = accountRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(VendorLoginRequest data)
    {
        
        if (data.email == null || data.password == null)
        {
            return BadRequest();
        }
        
        
        var vendor = await sellerRepository.GetByEmail(data.email);

        if (vendor == null || vendor.Account == null || !BCrypt.Net.BCrypt.Verify(data.password, vendor.Account.Password)) {
            
            return Forbid();
        }

        var resposeBody = new AuthResponse();
        resposeBody.access_token = AuthenticationService.createToken(vendor.Account);

        return Ok(resposeBody);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(VendorRegisterRequest data)
    {
        if (data.email == null || data.password == null || data.cnpj == null || data.name == null || data.surname == null)
        {
            return BadRequest();
        }

        if (await sellerRepository.GetByEmail(data.email) != null || await sellerRepository.GetByCNPJ(data.cnpj) != null)
        {
            Console.WriteLine("bunda 2");
            return BadRequest();
        }

        var vendor = await sellerRepository.Create(data.email, data.password, data.name, data.surname, data.cnpj);
        
        var responseBody = new AuthResponse();
        responseBody.access_token = AuthenticationService.createToken(vendor.Account);

        return Ok(responseBody);
    }
}
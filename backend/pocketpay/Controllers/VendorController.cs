
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/vendors")]
public class VendorController : ControllerBase
{
    private readonly IVendorRepository vendorRepository;
    private readonly IAccountRepository accountRepository;

    public VendorController(IVendorRepository vendor_repository, IAccountRepository account_repository)
    {
        vendorRepository = vendor_repository;
        accountRepository = account_repository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> login(VendorLoginRequest data)
    {
        
        if (data.email == null || data.password == null)
        {
            return BadRequest();
        }
        
        
        var vendor = await vendorRepository.GetByEmail(data.email);

        if (vendor == null || vendor.Account == null || !BCrypt.Net.BCrypt.Verify(data.password, vendor.Account.Password)) {
            
            return Forbid();
        }

        var resposeBody = new AuthResponse();
        resposeBody.access_token = AuthenticationService.createToken(vendor.Account);

        return Ok(resposeBody);
    }

    [HttpPost("register")]
    public async Task<IActionResult> register(VendorRegisterRequest data)
    {
        if (data.email == null || data.password == null || data.cnpj == null || data.name == null || data.surname == null)
        {
            return BadRequest();
        }

        if (await vendorRepository.GetByEmail(data.email) != null || await vendorRepository.GetByCNPJ(data.cnpj) != null)
        {
            return BadRequest();
        }

        var account = await accountRepository.Create(data.email, data.password, "Vendor");
        var vendor = await vendorRepository.Create(account, data.name, data.surname, data.cnpj);
        
        var responseBody = new AuthResponse();
        responseBody.access_token = AuthenticationService.createToken(account);

        return Ok(responseBody);
    }
}
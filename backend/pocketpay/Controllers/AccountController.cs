using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository accountRepository;
    private readonly IUserRepository userRepository;
    private readonly IVendorRepository vendorRepository;

    
    public AccountController(IAccountRepository accountRepository, IUserRepository userRepository, IVendorRepository vendorRepository)
    {
        this.accountRepository = accountRepository;
        this.userRepository = userRepository;
        this.vendorRepository = vendorRepository;
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        if (User.Identity == null || User.Identity.Name == null)
        {
            return Forbid();
        }   
        
        if (User.IsInRole("User"))
        {
            var user = await userRepository.GetByEmail(User.Identity.Name);

            if (user == null || user.Account == null) {return Unauthorized();}

            var responseBody = new UserProfileResponse();

            responseBody.email = user.Account.Email;
            responseBody.name = user.Name;
            responseBody.surname = user.Surname;
            responseBody.cpf = user.CPF;

            return Ok(responseBody);
        }

        if (User.IsInRole("Vendor"))
        {
            var vendor = await vendorRepository.GetByEmail(User.Identity.Name);

            if (vendor == null || vendor.Account == null) {return Unauthorized();}

            var responseBody = new VendorProfileResponse();
            responseBody.email = vendor.Account.Email;
            responseBody.name = vendor.Name;
            responseBody.surname = vendor.Surname;
            responseBody.cnpj = vendor.CPNJ;

            return Ok(responseBody);
        }

        return BadRequest();
    }
}
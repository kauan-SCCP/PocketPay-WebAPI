
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/profile")]
public class ProfileController : ControllerBase
{
    private readonly IClientRepository clientRepository;
    private readonly ISellerRepository sellerRepository;

    public ProfileController(IClientRepository clientRepository, ISellerRepository sellerRepository)
    {
        this.clientRepository = clientRepository;
        this.sellerRepository = sellerRepository;
    }
    

    private async Task<IActionResult> GetClientProfile()
    {
        var user = await clientRepository.GetByEmail(User.Identity.Name);

        if (user == null || user.Account == null) {return Unauthorized();}

        var responseBody = new ClientProfileResponse();

        responseBody.email = user.Account.Email;
        responseBody.name = user.Name;
        responseBody.surname = user.Surname;
        responseBody.cpf = user.CPF;

        return Ok(responseBody);
    }

    private async Task<IActionResult> GetSellerProfile()
    {
        var vendor = await sellerRepository.GetByEmail(User.Identity.Name);

        if (vendor == null || vendor.Account == null) {return Unauthorized();}

        var responseBody = new VendorProfileResponse();
        responseBody.email = vendor.Account.Email;
        responseBody.name = vendor.Name;
        responseBody.surname = vendor.Surname;
        responseBody.cnpj = vendor.CPNJ;

        return Ok(responseBody);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        
        if (User.IsInRole("Client"))
        {
            return await GetClientProfile();
        }

        if (User.IsInRole("Seller"))
        {
            return await GetSellerProfile();
        }

        return BadRequest();
    }
}
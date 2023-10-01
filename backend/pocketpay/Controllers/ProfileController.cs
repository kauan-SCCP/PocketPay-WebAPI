
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/profile")]
public class ProfileController : ControllerBase
{
    private readonly IAccountRepository accountRepository;
    private readonly IClientRepository clientRepository;
    private readonly ISellerRepository sellerRepository;

    public ProfileController(IAccountRepository accountRepository, IClientRepository clientRepository, ISellerRepository sellerRepository)
    {
        this.accountRepository = accountRepository;
        this.clientRepository = clientRepository;
        this.sellerRepository = sellerRepository;
    }

    public async Task<IActionResult> GetClientProfile(AccountModel account)
    {
        var client = await clientRepository.FindByAccount(account);
        if (client == null) {return NotFound();}

        var responsebody = new ClientProfileResponse()
        {
            name = client.Name,
            surname = client.Surname,
            cpf = client.CPF,
            email = account.Email
        };

        return Ok(responsebody);
    }

    public async Task<IActionResult> GetSellerProfile(AccountModel account)
    {
        var seller = await sellerRepository.FindByAccount(account);
        if (seller == null) {return NotFound();}

        var responsebody = new SellerProfileResponse()
        {
            email = account.Email,
            name = seller.Name,
            surname = seller.Surname,
            cnpj = seller.CNPJ
        };

        return Ok(responsebody);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        if (User.Identity == null || User.Identity.Name == null)
        {
            return Forbid();
        }

        var account = await accountRepository.FindByEmail(User.Identity.Name);

        if (account == null) {return Forbid();}

        if (User.IsInRole(AccountRole.Client.ToString()))
        {
            return await GetClientProfile(account);
        } 
        
        else 
        {
            return await GetSellerProfile(account);
        }
    }
}
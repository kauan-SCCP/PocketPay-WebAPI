using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
[Route("api/v1/wallet")]
public class WalletController : ControllerBase
{
    private readonly IWalletRepository _walletRepository;
    private readonly IAccountRepository _accountRepository;

    public WalletController(IWalletRepository walletRepository, IAccountRepository accountRepository)
    {
        _walletRepository = walletRepository;
        _accountRepository = accountRepository;
    }
    
    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> GetUserWallet()
    {
        var email = User.Identity.Name;
        if (email == null)
        {
            return BadRequest();
        }

        var account = await _accountRepository.FindByEmail(email);
        if (account == null)
        {
            return BadRequest();
        }
        var wallet = await _walletRepository.FindByAccount(account);

        var responseBody = new WalletResponse()
        {
            balance = wallet.Balance
        };
        return Ok(responseBody);
    }
}
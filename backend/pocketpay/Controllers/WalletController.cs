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
        var wallet =  _walletRepository.FindByAccount(account);

        var responseBody = new WalletResponse()
        {
            balance = wallet.Result.Balance
        };
        return Ok(responseBody);
    }


    [HttpPost("/deposit")]
    [Authorize]
    public async Task<IActionResult> Deposit(WalletDepositRequest data)
    {
        var email = User.Identity.Name;
        if (email == null || data.value == null || data.value <= 0)
        {
            return BadRequest();
        }

        var account = await _accountRepository.FindByEmail(email);
        if (account == null)
        {
            return BadRequest();
        }
        var wallet = await _walletRepository.FindByAccount(account);
        if (wallet == null)
        {
            return BadRequest();
        }

        var walletSave = await _walletRepository.Deposit(wallet.Id, (double)data.value);
        

        var responseBody = new WalletResponse()
        {
            balance = walletSave.Balance,
        };
        return Ok(responseBody);
    }

    [HttpPost("/withdraw")]
    [Authorize]
    public async Task<IActionResult> Withdraw(WalletDepositRequest data)
    {
        var email = User.Identity.Name;
        if (email == null || data.value == null)
        {
            return BadRequest();
        }
        var account = await _accountRepository.FindByEmail(email);
        if (account == null)
        {
            return BadRequest();
        }
        var wallet = await _walletRepository.FindByAccount(account);

        if (wallet == null || data.value > wallet.Balance)
        {
            return BadRequest();
        }

        var walletSave = await _walletRepository.Withdraw(wallet.Id, (double)data.value);

        var responseBody = new WalletResponse()
        {
            balance = walletSave.Balance,
        };
        return Ok(responseBody);
    }
}
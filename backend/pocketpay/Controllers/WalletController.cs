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
    private readonly ICardRepository _cardRepository;

    public WalletController(IWalletRepository walletRepository, IAccountRepository accountRepository, ICardRepository cardRepository)
    {
        _walletRepository = walletRepository;
        _accountRepository = accountRepository;
        _cardRepository = cardRepository;
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

    [HttpPut("deposit")]
    [Authorize]
    public async Task<IActionResult> Deposit(WalletDepositRequest data)
    {
        var email = User.Identity.Name;
        var card = await _cardRepository.FindById(data.cardId);
        if (email == null || data.value == null || data.value <= 0 || card == null)
        {
            return BadRequest();
        }

        

        CardPaymentService.Validate(card, (double)data.value);

        var account = await _accountRepository.FindByEmail(email);
        if (account == null || card.Account != account)
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

    [HttpPut("withdraw")]
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

        if (wallet == null)
        {
            return BadRequest();
        }

        if (data.value > wallet.Balance)
        {
            return Forbid();
        }

        var walletSave = await _walletRepository.Withdraw(wallet.Id, (double)data.value);

        var responseBody = new WalletResponse()
        {
            balance = walletSave.Balance,
        };
        return Ok(responseBody);
    }
}
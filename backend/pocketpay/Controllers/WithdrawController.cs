using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pocketpay.Models;

[ApiController]
[Route("api/v1/withdraw")]
public class WithdrawController : ControllerBase
{
    private readonly IWithdrawRepository _withdrawRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IWalletRepository _walletRepository;


    public WithdrawController(IWithdrawRepository withdrawRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository, IWalletRepository walletRepository)
    {
        _withdrawRepository = withdrawRepository;
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _walletRepository = walletRepository;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetWithdraws()
    {
        if (User.Identity == null || User.Identity.Name == null) return StatusCode(500);

        var account = await _accountRepository.FindByEmail(User.Identity.Name);
        if (account == null) return StatusCode(500);
    
        var withdraws = await _withdrawRepository.FindByAccount(account);

        var responseBody = new List<WithdrawResponse>();
        
        foreach (WithdrawModel w in withdraws)
        {            
            var foundWithdraws = new WithdrawResponse()
            {
                IdWithdraw = w.Id,
                IdTransaction = w.Transaction.Id,
                timestamp =  DateTime.Today,
                value = w.value
            };
            responseBody.Add(foundWithdraws);
        }

        return Ok(responseBody);
    }

    [HttpPost("")]
    [Authorize]
    public async Task<IActionResult> Withdraw(WithdrawResquest data)
    {
        if (User.Identity == null || User.Identity.Name == null) return StatusCode(500);
        
        if (data.value <= 0) return BadRequest();
    
        var account = await _accountRepository.FindByEmail(User.Identity.Name)
        ;
        if (account == null) return BadRequest();
        
        var wallet = await _walletRepository.FindByAccount(account);
        
        if (wallet == null) return StatusCode(500);

        var transaction = await _transactionRepository.Create(TransactionType.Withdraw, account);
        var withdraw = await _withdrawRepository.Create(account, transaction, data.value);
        
        if (data.value > wallet.Balance) return Forbid();

        await _walletRepository.Withdraw(wallet.Id, data.value);
        
        var responseBody = new WithdrawResponse()
        {
            IdWithdraw = withdraw.Id,
            IdTransaction = transaction.Id,
            timestamp =  DateTime.Today,
            value = data.value
        };

        return Ok(responseBody);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/deposit")]

public class DepositController : ControllerBase
{
    private readonly IDepositRepository depositRepository;
    private readonly IAccountRepository accountRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly IWalletRepository walletRepository;

    public DepositController(IDepositRepository depositRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository, IWalletRepository walletRepository)
    {
        this.depositRepository = depositRepository;
        this.accountRepository = accountRepository;
        this.transactionRepository = transactionRepository;
        this.walletRepository = walletRepository;
    }

    [HttpGet]
    [Authorize]

    public async Task<IActionResult> GetDeposits()
    {
        if (User.Identity == null || User.Identity.Name == null)
        {
            return StatusCode(500);
        }

        var account = await this.accountRepository.FindByEmail(User.Identity.Name);

        var deposits = await this.depositRepository.FindByAccount(account);

        var responseBody = new List<DepositResponse>();

        foreach (DepositModel d in deposits)
        {
            var foundDeposit = new DepositResponse()
            {
                id = d.Id,
                timestamp = d.Transaction.TimeStamp,
                transaction = d.Transaction.Id,
                value = d.Value

            };

            responseBody.Add(foundDeposit);
        }
        
	return Ok(responseBody);   
    }

    [HttpPost]
    [Authorize]

    public async Task<IActionResult> Deposit(DepositRequest data)
    {

        if (User.Identity == null || User.Identity.Name == null)
        {
            return StatusCode(500);
        }

        var account = await accountRepository.FindByEmail(User.Identity.Name);
        
        var transaction = await this.transactionRepository.Create(TransactionType.Deposit, account);

        var deposit = await depositRepository.Create(transaction, account, data.value);

        var wallet = await walletRepository.FindByAccount(account);

        var updatedWallet = await walletRepository.Deposit(wallet.Id, data.value);

        var depositInfo = new
        {
            id = transaction.Id,
            balance = updatedWallet.Balance,
        };

        return Ok(depositInfo);


    }




}

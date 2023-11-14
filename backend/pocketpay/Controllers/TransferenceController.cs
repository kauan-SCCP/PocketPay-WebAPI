

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pocketpay.Models;
using pocketpay.Repositories.Interfaces;

[ApiController]
[Route("api/v1/transference")]
public class TransferenceController : ControllerBase
{

    private readonly IAccountRepository accountRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly ITransferenceRepository transferenceRepository;
    private readonly IWalletRepository walletRepository;
    
    public TransferenceController(IAccountRepository accountRepository, ITransferenceRepository transferenceRepository, ITransactionRepository transactionRepository, IWalletRepository walletRepository)
    {
        this.accountRepository = accountRepository;
        this.transferenceRepository = transferenceRepository;
        this.transactionRepository = transactionRepository;
        this.walletRepository = walletRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTransferences()
    {
        if (User.Identity == null || User.Identity.Name == null) return StatusCode(500);

        var account = await accountRepository.FindByEmail(User.Identity.Name);
        if (account == null) return StatusCode(500);

        var transferences = await transferenceRepository.FindByAccount(account);

        var responseBody = new List<TransferenceResponse>();

        foreach (TransferenceModel t in transferences)
        {
            var transferenceResponse = new TransferenceResponse()
            {
                id = t.Id,
                transaction = t.Transaction.Id,
                timestamp = t.Transaction.TimeStamp,
                value = t.Value,
                receiver = t.Receiver.Email,
                sender = t.Sender.Email
            };

            responseBody.Add(transferenceResponse);
        }

        return Ok(responseBody);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Transfer(TransferenceRequest data)
    {
        if (User.Identity == null || User.Identity.Name == null) return StatusCode(500);

        if (data.value <= 0 || data.receiver == null) return BadRequest();

        var account = await accountRepository.FindByEmail(User.Identity.Name);
        if (account == null) return StatusCode(500);

        if (User.Identity.Name == data.receiver) return Unauthorized();

        var receiver = await accountRepository.FindByEmail(data.receiver);
        if (receiver == null) return BadRequest();

        var walletSender = await walletRepository.FindByAccount(account);
        var walletReceiver = await walletRepository.FindByAccount(receiver);

        if (walletSender == null || walletReceiver == null) return StatusCode(500);
        if (walletSender.Balance <= 0) return Unauthorized();

        var transaction = await transactionRepository.Create(TransactionType.Transference, account);
        var transference = await transferenceRepository.Create(transaction, account, receiver, data.value);

        await walletRepository.Withdraw(walletSender.Id, data.value);
        await walletRepository.Deposit(walletReceiver.Id, data.value);

        var responseBody = new TransferenceResponse()
        {
            id = transference.Id,
            transaction = transference.Transaction.Id,
            timestamp = transference.Transaction.TimeStamp,
            value = transference.Value,
            receiver = transference.Receiver.Email,
            sender = transference.Sender.Email
        };

        return Ok(responseBody);
    }
}
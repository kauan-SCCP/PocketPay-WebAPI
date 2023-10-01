using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pocketpay.DTOs.Transaction;
using pocketpay.Models;
using System.Security.Cryptography.Xml;

namespace pocketpay.Controllers;

[ApiController]
[Route("api/v1/transaction")]
public class TransactionController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    public TransactionController(IAccountRepository accountRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository)
    {
        this._accountRepository = accountRepository;
        this._walletRepository = walletRepository;
        this._transactionRepository = transactionRepository;
    }

    [HttpPost("")]
    [Authorize]

    public async Task<IActionResult> CreateTransaction(TransactionRegisterRequest request)
    {
        var email = User.Identity.Name;
        var account = await _accountRepository.FindByEmail(email);
        var wallet = await _walletRepository.FindByAccount(account);
        var receiver = await _accountRepository.FindByEmail(request.email_receiver);


        if (account == null || wallet.Balance < request.value || request.value <= 0)
        {
            return BadRequest();
        }

        var newTransaction = _transactionRepository.Create(account, receiver, request.value); //registro minha transação
        await _walletRepository.Withdraw(wallet.Id, request.value); // pego qual é a minha carteira
        var walletReceiver = await _walletRepository.FindByAccount(receiver); // pego a carteira do destinatario
        _walletRepository.Deposit(walletReceiver.Id, request.value); // realizo o deposito

        return Ok();
    }

    [HttpGet("")]
    [Authorize]

    public async Task<IActionResult> GerUserTransaction() 
    {
        var email = User.Identity.Name;
        var account = await _accountRepository.FindByEmail(email);

        if (account == null)
        {
            return BadRequest();
        }

        var AllTransaction = await _transactionRepository.FindBySender(account);
        var responseBory = new List<TransactionResponse>();

        foreach (TransactionModel T in AllTransaction)
        {
            var transaction = new TransactionResponse()
            {
                receiverEmail = T.To.Email,
                timeStamp = T.TimeStamp,
                senderEmail = T.From.Email,
                value = T.Value
            };

            responseBory.Add(transaction);
        }
        return Ok(responseBory);
    }
        


}

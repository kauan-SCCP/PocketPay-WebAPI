using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using pocketpay.DTOs.Transaction;
using pocketpay.Models;
using System.Security.Cryptography.Xml;

namespace pocketpay.Controllers;

[ApiController]
[Route("api/v1/transaction")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IAccountRepository accountRepository;

    public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        this.transactionRepository = transactionRepository;
        this.accountRepository = accountRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetTransactions()
    {
        if (User.Identity == null || User.Identity.Name == null )
        {
            return StatusCode(500);
        }

        var account = await accountRepository.FindByEmail(User.Identity.Name);
        if (account == null)
        {
            return StatusCode(500);
        }

        var transactions = await transactionRepository.FindByOwner(account);

        var responseBody = new List<TransactionResponse>();
        
        foreach (TransactionModel t in transactions)
        {
            string _type;
            if (t.Type == TransactionType.Deposit)
            {
                _type = "Deposit";
            } else if(t.Type == TransactionType.Transference)
            {
                _type = "Transference";
            } else if(t.Type == TransactionType.Withdraw)
            {
                _type = "Withdraw";
            }else {
                _type = "null";
            };
            var foundTransaction = new TransactionResponse()
            {
                id = t.Id,
                timestamp =  t.TimeStamp,
                type = _type
            };

            responseBody.Add(foundTransaction);
        }

        return Ok(responseBody);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetTransactionById(Guid id)
    {
        if (User.Identity == null || User.Identity.Name == null )
        {
            return StatusCode(500);
        }

        var account = await accountRepository.FindByEmail(User.Identity.Name);
        if (account == null)
        {
            return StatusCode(500);
        }

        var transaction = await transactionRepository.FindById(id);

        if (transaction == null)
        {
            return NotFound();
        }
        string _type;
            if (transaction.Type == TransactionType.Deposit)
            {
                _type = "Deposit";
            } else if(transaction.Type == TransactionType.Transference)
            {
                _type = "Transference";
            } else if(transaction.Type == TransactionType.Withdraw)
            {
                _type = "Withdraw";
            }else {
                _type = "null";
            };

        var responseBody = new TransactionResponse()
        {
            id = transaction.Id,
            timestamp = transaction.TimeStamp,
            type = _type
        };

        return Ok(responseBody);
    }
}

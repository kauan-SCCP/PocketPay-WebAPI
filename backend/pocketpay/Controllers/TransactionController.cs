
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/transaction")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Register(TransactionRegisterRequest data)
    {

        if (data.toMail == null || data.value == null)
        {
            return BadRequest();
        }


        //RESOLVER ERRO DE BAD REQUEST!!!!
        if (await _accountRepository.GetByEmail(data.toMail) == null)
        {
            return BadRequest();
        }

        var transaction = await _transactionRepository.Create(User.Identity.Name, data.toMail, data.value);
        
        return Ok("Recebido por:" + JsonSerializer.Serialize(_accountRepository.GetByEmail(data.toMail)));
    }
}
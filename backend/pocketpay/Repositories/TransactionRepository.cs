using System.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using pocketpay.Models;

public class TransactionRepository : ITransactionRepository
{
    private BankContext _context;
    public TransactionRepository(BankContext context) 
    {
        _context = context;
     
    }

    public async Task<TransactionModel> Create(AccountModel sender, AccountModel receiver, double value)
    {
        //Vai ser um HttpPost - SaveChangesAsync();
        var newTransaction = new TransactionModel();

        newTransaction.From = sender;
        newTransaction.To = receiver;
        newTransaction.Value = value;
        newTransaction.TimeStamp = DateTime.UtcNow;

        await _context.AddAsync(newTransaction);
        await _context.SaveChangesAsync();

        return newTransaction;
    }

    public async Task<TransactionModel?> FindByAccount(AccountModel account)
    {
        //Vai ser um HttpGet
        var transaction = await _context.Transactions // passa a tabela para a variavel
            .Include(transaction => transaction.From) // me traga dessa tabela quem fez a transição
            .FirstOrDefaultAsync(transaction => transaction.From == account); // na tabela tran busque pelo From, se for igual ao parametro é OK

        return transaction;
    }

    public async Task<TransactionModel?> FindById(Guid id)
    {
        var transaction = await _context.Transactions
            .Include(transaction => transaction.Id)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);

        return transaction;
    }

    public async Task<IEnumerable<TransactionModel>> FindByReceiver(AccountModel receiver)
    {
        var transaction = await _context.Transactions
            .Include(transaction => transaction.To)
            .Where(transaction => transaction.To == receiver)
            .ToListAsync();

        return transaction;
    }


    public async Task<IEnumerable<TransactionModel>> FindBySender(AccountModel sender)
    {
        var transaction = await _context.Transactions
            .Include(transaction => transaction.From)
            .Where(transaction => transaction.From == sender)
            .ToListAsync();

        return transaction;
    }
}
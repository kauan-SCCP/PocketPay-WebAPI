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

    public async Task<TransactionModel> Create(TransactionType type, AccountModel owner)
    {
        var transaction = new TransactionModel()
        {
            Id = new Guid(),
            TimeStamp = DateTime.Now,
            Status = TransactionStatus.Pending,
            Type = type,
            Owner = owner
        };

        await _context.AddAsync(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }

    public async Task<TransactionModel?> FindById(Guid id)
    {
        var transaction = await _context.Transactions
                .Include(transaction => transaction.Owner)
                .FirstOrDefaultAsync(transaction => transaction.Id == id);

        return transaction;
    }

    public async Task<IEnumerable<TransactionModel>> FindByOwner(AccountModel owner)
    {
        var transactions = await _context.Transactions
                .Include(transaction => transaction.Owner)
                .Where(transaction => transaction.Owner == owner)
                .ToArrayAsync();

        return transactions;
    }
}
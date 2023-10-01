


using Microsoft.EntityFrameworkCore;

public class ExternalTransactionRepository : IExternalTransactionRepository
{
    private readonly BankContext _context;

    public ExternalTransactionRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<ExternalTransactionModel> Create(AccountModel account, double value)
    {
        var newExternalTransaction = new ExternalTransactionModel()
        {
            Id = new Guid(),
            Status = ExternalTransactionStatus.Pending,
            Timestamp = DateTime.Now,
            Value = value,
            Account = account
        };

        await _context.AddAsync(newExternalTransaction);
        await _context.SaveChangesAsync();

        return newExternalTransaction;
    }

    public async Task<ExternalTransactionModel?> FindByAccount(AccountModel account)
    {
        var external = await _context.ExternalTransactions
                            .Include(external => external.Account)
                            .FirstOrDefaultAsync(external => external.Account == account);

        return external;
    }

    public async Task<ExternalTransactionModel?> FindById(Guid id)
    {
        var external = await _context.ExternalTransactions
                    .Include(external => external.Account)
                    .FirstOrDefaultAsync(external => external.Id == id);

        return external;
    }

    public async Task<ExternalTransactionModel?> UpdateStatus(Guid id, ExternalTransactionStatus status)
    {
        var external = await FindById(id);

        if (external == null) {return null;}

        external.Status = status;
        await _context.SaveChangesAsync();

        return external;
    }
}
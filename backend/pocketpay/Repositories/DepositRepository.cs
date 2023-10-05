using Microsoft.EntityFrameworkCore;
using pocketpay.Models;

public class DepositRepository : IDepositRepository
{
    private BankContext _context;

    public DepositRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<DepositModel> Create(TransactionModel transaction, AccountModel account, double value)
    {
        var newDeposit = new DepositModel();

        newDeposit.Id = new Guid();
        newDeposit.Transaction = transaction;
        newDeposit.Account = account;
        newDeposit.Value = value;

        await _context.AddAsync(newDeposit);
        await _context.SaveChangesAsync();

        return newDeposit;
    }

    public async Task<DepositModel?> FindById(Guid id)
    {
        var deposit = await _context.Deposits
            .Include(deposit => deposit.Account)
            .Include(deposit => deposit.Transaction)
            .FirstOrDefaultAsync(deposit => deposit.Id == id);

        return deposit;
      
    }
    public async Task<DepositModel?> FindByTransaction(TransactionModel transaction)
    {
        var deposit = await _context.Deposits
            .Include(deposit => deposit.Account)
            .Include(deposit => deposit.Transaction)
            .FirstOrDefaultAsync(deposit => deposit.Transaction == transaction);

        return deposit;
    }


    public async Task<IEnumerable<DepositModel>> FindByAccount(AccountModel account)
    {
        var deposit = await _context.Deposits
            .Include(deposit => deposit.Account)
            .Include(deposit => deposit.Transaction)
            .Where(deposit => deposit.Account == account).ToListAsync();

        return deposit;

    }
}

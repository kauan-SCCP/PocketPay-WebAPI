using Microsoft.EntityFrameworkCore;
using pocketpay.Models;

public class WithdrawRepository : IWithdrawRepository
{
    private BankContext _context;

    //Acessa o BankContext para retornar uma cópia do contexto por injeção de dependência.
    public WithdrawRepository(BankContext context) 
    {
        _context = context;
    }

    public async Task<WithdrawModel> Create(AccountModel account, TransactionModel transaction, double value)
    {
        var newWithdraw = new WithdrawModel();
        newWithdraw.Id = new Guid();
        newWithdraw.Transaction = transaction;
        newWithdraw.Account = account;
        newWithdraw.value = value;
        await _context.AddAsync(newWithdraw);
        await _context.SaveChangesAsync();
        return newWithdraw;
    }

    public async Task<WithdrawModel?> FindById(Guid id)
    {
        var withdraw = await _context.Withdraws
            .Include(withdraw => withdraw.Account)
            .Include(withdraw => withdraw.Transaction)
            .FirstOrDefaultAsync(withdraw => withdraw.Id == id);

        return withdraw;
    }

    public async Task<WithdrawModel?> FindByTransaction(TransactionModel transaction)
    {
        var withdraw = await _context.Withdraws
            .Include(withdraw => withdraw.Account)
            .Include(withdraw => withdraw.Transaction)
            .FirstOrDefaultAsync(withdraw => withdraw.Transaction == transaction);
        return withdraw;
    }

    public async Task<IEnumerable<WithdrawModel>> FindByAccount(AccountModel account)
    {
        var withdraw = await _context.Withdraws
            .Include(withdraw => withdraw.Account)
            .Include(withdraw => withdraw.Transaction)
            .Where(withdraw => withdraw.Account == account).ToListAsync();

        return withdraw;
    }

    
}
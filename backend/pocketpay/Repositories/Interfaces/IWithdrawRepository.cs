using pocketpay.Models;

public interface IWithdrawRepository
{
    public Task<WithdrawModel> Create(AccountModel account, TransactionModel transaction, double value);
    public Task<WithdrawModel?> FindById(Guid id);
    public Task<WithdrawModel?> FindByTransaction(TransactionModel account);
    public Task<IEnumerable<WithdrawModel>> FindByAccount(AccountModel account);
}
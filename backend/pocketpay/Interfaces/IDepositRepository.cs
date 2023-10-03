using pocketpay.Models;

public interface IDepositRepository
{
    public Task<DepositModel> Create(TransactionModel transaction, AccountModel account, double value);
    public Task<DepositModel?> FindById(Guid id);
    public Task<DepositModel?> FindByTransaction(TransactionModel transaction);
    public Task<IEnumerable<DepositModel>> FindByAccount(AccountModel account);
}

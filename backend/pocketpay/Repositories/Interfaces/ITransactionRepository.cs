using pocketpay.Models;

public interface ITransactionRepository
{
    public Task<TransactionModel> Create(AccountModel sender, AccountModel receiver, double value);
    public Task<TransactionModel?> FindById(Guid id);
    public Task<TransactionModel?> FindByAccount(AccountModel account);
    public Task<IEnumerable<TransactionModel>> FindBySender(AccountModel sender);
    public Task<IEnumerable<TransactionModel>> FindByReceiver(AccountModel receiver);
}
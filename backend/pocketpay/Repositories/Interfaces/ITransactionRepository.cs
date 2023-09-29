using pocketpay.Models;

public interface ITransactionRepository
{
    public Task<TransactionModel> Create(AccountModel sender, AccountModel receiver);
    public Task<TransactionModel?> FindById(Guid id);
    public Task<TransactionModel?> FindByAccount(AccountModel account);
    public Task<TransactionModel?> FindBySender(AccountModel sender);
    public Task<TransactionModel?> FindByReceiver(AccountModel receiver);
    public Task<TransactionModel?> Revert(Guid id);
}
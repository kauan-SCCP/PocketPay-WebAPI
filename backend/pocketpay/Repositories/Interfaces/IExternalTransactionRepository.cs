public interface IExternalTransactionRepository
{
    public Task<ExternalTransactionModel> Create(AccountModel account, double value);
    public Task<ExternalTransactionModel?> FindById(Guid id);
    public Task<ExternalTransactionModel?> FindByAccount(AccountModel account);
    public Task<ExternalTransactionModel?> UpdateStatus(Guid id, ExternalTransactionStatus status);
}
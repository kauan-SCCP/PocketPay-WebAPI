using System.Security.Cryptography.X509Certificates;
using pocketpay.Models;

public interface ITransactionRepository
{
    public Task<TransactionModel> Create(TransactionType type, AccountModel owner);
    public Task<TransactionModel?> FindById(Guid id);
    public Task<IEnumerable<TransactionModel>> FindByOwner(AccountModel owner);
}
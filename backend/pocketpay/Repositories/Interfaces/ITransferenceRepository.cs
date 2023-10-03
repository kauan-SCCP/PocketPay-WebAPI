using pocketpay.Models;

namespace pocketpay.Repositories.Interfaces
{
    public interface ITransferenceRepository
    {
        public Task<TransferenceModels> Create(AccountModel sender, AccountModel receiver, double value);
        public Task<TransferenceModels> FindById(Guid id); //Buscar um transação especifica
        public Task<TransferenceModels> FindByTransaction(TransactionModel transaction); //
        public Task<IEnumerable<TransferenceModels>> FindByAccount(AccountModel account);
        public Task<IEnumerable<TransferenceModels>> FindBySender(AccountModel sender);
        public Task<IEnumerable<TransferenceModels>> FindByReceiver(AccountModel receiver);
        public Task<TransferenceModels> Revert(TransferenceModels transference);

    }
}

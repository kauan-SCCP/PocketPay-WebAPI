using pocketpay.Models;

namespace pocketpay.Repositories.Interfaces
{
    public interface ITransferenceRepository
    {
        public Task<TransferenceModel> Create(TransactionModel transaction, AccountModel sender, AccountModel receiver, double value);
        public Task<TransferenceModel> FindById(Guid id); //Buscar um transação especifica
        public Task<TransferenceModel> FindByTransaction(TransactionModel transaction); //
        public Task<IEnumerable<TransferenceModel>> FindByAccount(AccountModel account);
        public Task<IEnumerable<TransferenceModel>> FindBySender(AccountModel sender);
        public Task<IEnumerable<TransferenceModel>> FindByReceiver(AccountModel receiver);
        public Task<TransferenceModel> Revert(TransferenceModel transference);

    }
}

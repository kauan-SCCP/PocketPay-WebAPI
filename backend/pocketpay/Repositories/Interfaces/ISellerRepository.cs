public interface ISellerRepository
{
    public Task<SellerModel> Create(AccountModel account, string name, string surname, string cpnj);
    public Task<SellerModel?> FindById(Guid id);
    public Task<SellerModel?> FindByCNPJ(string cpnj);
    public Task<ClientModel?> FindByAccount(AccountModel account);
    public Task<SellerModel?> Update(Guid id, AccountModel account, string name, string surname, string cpnj);
    public Task<SellerModel?> Delete(Guid id);
}
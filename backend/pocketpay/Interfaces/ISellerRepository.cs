public interface ISellerRepository
{
    public Task<SellerModel> Create(AccountModel account, string name, string surname, string cnpj);
    public Task<SellerModel?> FindById(Guid id);
    public Task<SellerModel?> FindByCNPJ(string cnpj);
    public Task<SellerModel?> FindByAccount(AccountModel account);
    public Task<SellerModel?> Update(Guid id, AccountModel account, string name, string surname, string cnpj);
    public Task<SellerModel?> Delete(Guid id);
}
public interface IAccountRepository
{
    public Task<AccountModel> Create(string email, string password, AccountRole role);
    public Task<AccountModel?> FindById(Guid id);
    public Task<AccountModel?> FindByEmail(string email);
    public Task<AccountModel?> Update(Guid id, string email, string password, AccountRole role);
    public Task<AccountModel?> Delete(Guid id);
}
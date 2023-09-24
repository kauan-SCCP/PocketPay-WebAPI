public interface IAccountRepository
{
    public Task<AccountModel> Create(string email, string password, string role);
    public Task<AccountModel> GetById(Guid id);
    public Task<AccountModel> GetByEmail(string email);
    public Task<AccountModel> Delete(Guid id);
}
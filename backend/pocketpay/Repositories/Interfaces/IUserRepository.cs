public interface IUserRepository
{
    public Task<UserModel> Create(string email, string password, string name, string surname, string cpf);
    public Task<UserModel> GetById(Guid id);
    public Task<UserModel> GetByEmail(string email);
    public Task<UserModel> GetByCPF(string cpf);
    public Task<UserModel> UpdateById(Guid id, AccountModel account, string name, string surname, string cpf);
    public Task<UserModel> Delete(Guid id);
}
public interface IClientRepository
{
    public Task<ClientModel> Create(AccountModel account, string name, string surname, string cpf);
    public Task<ClientModel?> FindById(Guid id);
    public Task<ClientModel?> FindByCPF(string cpf);
    public Task<ClientModel?> FindByAccount(AccountModel account);
    public Task<ClientModel?> Update(Guid id, AccountModel account, string name, string surname, string cpf);
    public Task<ClientModel?> Delete(Guid id);
}
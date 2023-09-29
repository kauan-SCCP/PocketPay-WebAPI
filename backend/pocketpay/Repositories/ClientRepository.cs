using Microsoft.EntityFrameworkCore;

public class ClientRepository : IClientRepository
{
    private readonly BankContext _context;

    public ClientRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<ClientModel> Create(AccountModel account, string name, string surname, string cpf)
    {
        var newClient = new ClientModel();
        
        newClient.Id = new Guid();
        newClient.Account = account;
        newClient.Name = name;
        newClient.Surname = surname;
        newClient.CPF = cpf;

        await _context.AddAsync(newClient);
        await _context.SaveChangesAsync();

        return newClient;
    }

    public async Task<ClientModel?> FindById(Guid id)
    {
        var client = await _context.Clients
            .Include(client => client.Account)
            .FirstOrDefaultAsync(client => client.Id == id);

        return client;
    }

    public async Task<ClientModel?> FindByCPF(string cpf)
    {
        var client = await _context.Clients
            .Include(client => client.Account)
            .FirstOrDefaultAsync(client => client.CPF == cpf);
        
        return client;
    }

    public async Task<ClientModel?> FindByAccount(AccountModel account)
    {
        var client = await _context.Clients
            .Include(client => client.Account)
            .FirstOrDefaultAsync(client => client.Account == account);

        return client;
    }

    public async Task<ClientModel?> Update(Guid id, AccountModel account, string name, string surname, string cpf)
    {
        var client = await FindById(id);

        if (client == null) {return null;}

        client.Account = account;
        client.Name = name;
        client.Surname = surname;
        client.CPF = cpf;
        await _context.SaveChangesAsync();

        return client;
    }

    public async Task<ClientModel?> Delete(Guid id)
    {
        var client = await FindById(id);
        if (client == null) {return null;}

        _context.Remove(client);
        await _context.SaveChangesAsync();

        return client;
    }

}
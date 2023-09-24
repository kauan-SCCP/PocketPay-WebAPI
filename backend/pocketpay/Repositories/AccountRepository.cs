

using Microsoft.EntityFrameworkCore;

public class AccountRepository : IAccountRepository
{
    private readonly BankContext _context;

    public AccountRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<AccountModel> Create(string email, string password, string role)
    {
        var newAccount = new AccountModel();
        
        newAccount.Id = new Guid();
        newAccount.Email = email;
        newAccount.Password = BCrypt.Net.BCrypt.HashPassword(password);
        newAccount.Role = role;

        await _context.AddAsync(newAccount);
        await _context.SaveChangesAsync();
        return newAccount;
    }

    public async Task<AccountModel> GetById(Guid id)
    {
        var account = await _context.Accounts.FindAsync(id);
        return account;
    }

    public async Task<AccountModel> GetByEmail(string email)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(
            account => account.Email == email
        );

        return account;
    }

    public async Task<AccountModel> UpdateById(Guid id, string email, string password, string role)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) {return null;}

        account.Email = email;
        account.Password = BCrypt.Net.BCrypt.HashPassword(password);
        account.Role = role;

        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<AccountModel> Delete(Guid id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null) {return null;}

        _context.Remove(account);

        return account;
    }


}
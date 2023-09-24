
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly BankContext _context;

    public UserRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<UserModel> createUser(UserRegisterRequest data) {
        if (await this.checkEmail(data.email) || await this.checkCPF(data.cpf)) {
            return null;
        }
        
        var newAccount = new AccountModel();
        newAccount.Id = new Guid();
        newAccount.Email = data.email;
        newAccount.Password = BCrypt.Net.BCrypt.HashPassword(data.password);
        newAccount.Role = "User";
        await _context.AddAsync(newAccount);
        await _context.SaveChangesAsync();

        var newUser = new UserModel();
        newUser.Id = new Guid();
        newUser.Name = data.name;
        newUser.Surname = data.surname;
        newUser.CPF = data.cpf;
        newUser.Account = newAccount;

        await _context.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<AccountModel> checkCredentials(String email, String password)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(
            account => account.Email == email
        );

        if (account == null || !BCrypt.Net.BCrypt.Verify(password, account.Password)) {
            return null;
        }

        return account;
    }

    private async Task<bool> checkEmail(string email)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(
            account => account.Email == email
        );

        if (account == null) {return false;}
        return true;
    }

    private async Task<bool> checkCPF(String cpf)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            user => user.CPF == cpf
        );

        if (user == null) {return false;}
        return true;
    }

}
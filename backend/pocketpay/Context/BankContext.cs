using Microsoft.EntityFrameworkCore;

public class BankContext : DbContext
{
    private readonly string DbPath = "./Database/Bank.sqlite3";

    public DbSet<AccountModel> Accounts {get; set;}
    public DbSet<UserModel> Users {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

}
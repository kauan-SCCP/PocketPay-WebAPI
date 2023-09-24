using Microsoft.EntityFrameworkCore;
using pocketpay.Models;

public class BankContext : DbContext
{
    private readonly string DbPath = "./Database/Bank.sqlite3";

    public DbSet<AccountModel> Accounts {get; set;}
    public DbSet<UserModel> Users {get; set;}
    public DbSet<VendorModel> Vendors {get; set;}

    public DbSet<WalletModel> Wallet { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

}
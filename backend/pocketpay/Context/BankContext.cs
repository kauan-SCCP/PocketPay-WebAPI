using Microsoft.EntityFrameworkCore;
using pocketpay.Models;

public class BankContext : DbContext
{
    private readonly string DbPath = "./Database/Bank.sqlite3";

    public DbSet<AccountModel> Accounts {get; set;}
    public DbSet<ClientModel> Clients {get; set;}
    public DbSet<SellerModel> Sellers {get; set;}

    public DbSet<WalletModel> Wallets {get; set;}
    public DbSet<TransactionModel> Transactions {get; set;}
    public DbSet<TransferenceModels> Transferences {get; set;}
    public DbSet<WithdrawModel> Withdraws {get; set;}
    public DbSet<DepositModel> Deposits {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

}
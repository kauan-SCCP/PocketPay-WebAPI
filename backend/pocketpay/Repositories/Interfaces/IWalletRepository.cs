using System.Transactions;
using pocketpay.Models;

public interface IWalletRepository
{
    public Task<WalletModel> getAccountWallet(string email);
}
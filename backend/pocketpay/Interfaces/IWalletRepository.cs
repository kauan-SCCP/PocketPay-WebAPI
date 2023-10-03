using pocketpay.Models;

public interface IWalletRepository
{
    public Task<WalletModel> Create(AccountModel account);
    public Task<WalletModel?> FindById(Guid id);
    public Task<WalletModel?> FindByAccount(AccountModel account);
    public Task<WalletModel?> Deposit(Guid id, double value);
    public Task<WalletModel?> Withdraw(Guid id, double value);
    public Task<WalletModel?> Delete(Guid id);
}
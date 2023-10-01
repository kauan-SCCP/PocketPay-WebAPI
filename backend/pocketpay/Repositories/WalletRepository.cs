using Microsoft.EntityFrameworkCore;
using pocketpay.Models;

public class WalletRepository : IWalletRepository
{
    private BankContext _context;

    //Acessa o BankContext para retornar uma cópia do contexto por injeção de dependência.
    public WalletRepository(BankContext context) 
    {
        _context = context;
    }

    public async Task<WalletModel> Create(AccountModel account)
    {
        var newWallet = new WalletModel();

        newWallet.Id = new Guid();
        newWallet.Account = account;
        newWallet.Balance = 0;

        await _context.AddAsync(newWallet);
        await _context.SaveChangesAsync();

        return newWallet;
    }

    public async Task<WalletModel?> Delete(Guid id)
    {
        var wallet = await FindById(id);
        if (wallet == null) {return null;}

        _context.Remove(wallet);
        await _context.SaveChangesAsync();

        return wallet;
    }

    public async Task<WalletModel?> Deposit(Guid id, double value)
    {
        var wallet = await FindById(id);
        if (wallet == null) {return null;}

        wallet.Balance += value;

        _context.Update(wallet);
        await _context.SaveChangesAsync();

        return wallet;
    }

    public async Task<WalletModel?> FindByAccount(AccountModel account)
    {
        var wallet = await _context.Wallets
            .Include(wallet => wallet.Account)
            .FirstOrDefaultAsync(wallet => wallet.Account == account);

        return wallet;
    }

    public async Task<WalletModel?> FindById(Guid id)
    {
        var wallet = await _context.Wallets
            .Include(wallet => wallet.Account)
            .FirstOrDefaultAsync(wallet => wallet.Id == id);

        return wallet;
    }


    public async Task<WalletModel?> Withdraw(Guid id, double value)
    {
        var wallet = await FindById(id);
        if (wallet == null) {return null;}

        wallet.Balance -= value;

        _context.Update(wallet);
        await _context.SaveChangesAsync();

        return wallet;
    }
}
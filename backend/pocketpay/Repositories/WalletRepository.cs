using System.Transactions;
using Microsoft.EntityFrameworkCore;
using pocketpay.Models;

public class WalletRepository : IWalletRepository
{
    private IAccountRepository _accountRepository;
    private BankContext _context;

    //Acessa o BankContext para retornar uma cópia do contexto por injeção de dependência.
    public WalletRepository(BankContext context, IAccountRepository accountRepository) 
    {
        _context = context;
        _accountRepository = accountRepository;
    }

    //Essa função serve para retornar todas as transações de um usuário através de seu email.

    public async Task<WalletModel> getAccountWallet(string _email)
    {
        var wallet = await  _context.Wallet 
            .Include(wallet => wallet.Account)
            .FirstOrDefaultAsync(wallet => wallet.Account.Email == _email);
        
        return wallet;
    }
}
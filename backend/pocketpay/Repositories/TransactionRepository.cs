using System.Transactions;
using pocketpay.Models;

public class TransactionRepository : ITransactionRepository
{
    private IAccountRepository _accountRepository;
    private BankContext _context;

    private WalletRepository _walletRepository;

    //Acessa o BankContext para retornar uma cópia do contexto por injeção de dependência.
    public TransactionRepository(BankContext context, IAccountRepository accountRepository, WalletRepository walletRepository) 
    {
        _context = context;
        _accountRepository = accountRepository;
        _walletRepository = walletRepository;
    }

    //Essa função serve para retornar todas as transações de um usuário através de seu email.
    public async Task<TransactionModel> CreateTransaction(string from, string to, double value)
    {
        var newTransaction = new TransactionModel();
        //Aqui a gente insere o usuário que fará a transferência
        newTransaction.From = await _accountRepository.GetByEmail(from); 
        newTransaction.To = await _accountRepository.GetByEmail(to);//Aqui a gente insere o usuário que receberá a transferência
        newTransaction.TimeStamp = DateTime.Now;
        newTransaction.Value = value;
        WalletModel actualFrom = await _walletRepository.getAccountWallet(from);
        WalletModel actualTo = await _walletRepository.getAccountWallet(to);
        if (actualFrom.Balance < value)
        {
            throw new Exception("Vai trabalhar vagabundo");
        }
        actualFrom.Balance += value;
        actualTo.Balance -= value;
        await _context.AddAsync(actualFrom);
        await _context.AddAsync(actualTo);
        await _context.AddAsync(newTransaction);
        await _context.SaveChangesAsync();
        return newTransaction;
    }
}
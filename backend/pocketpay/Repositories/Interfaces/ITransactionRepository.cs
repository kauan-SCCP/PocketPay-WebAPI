using System.Transactions;
using pocketpay.Models;

public interface ITransactionRepository
{
    public Task<TransactionModel> CreateTransaction(string from, string to, double value);
}
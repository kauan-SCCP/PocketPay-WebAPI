using System.Transactions;
using pocketpay.Models;

public interface ITransactionRepository
{
    public Task<TransactionModel> Create(string from, string to, double value);
}
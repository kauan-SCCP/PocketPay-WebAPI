namespace pocketpay.DTOs.Transaction;

public class TransactionResponse
{
    public Guid id {get; set;}
    public DateTime timestamp {get; set;}
    public TransactionType type {get; set;}
    public TransactionStatus status {get; set;}
}

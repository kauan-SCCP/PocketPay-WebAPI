namespace pocketpay.DTOs.Transaction;

public class TransactionResponse
{
    public Guid id {get; set;}
    public DateTime timestamp {get; set;}
    public required string type {get; set;}
}

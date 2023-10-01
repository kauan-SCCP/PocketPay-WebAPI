namespace pocketpay.DTOs.Transaction;

public class TransactionResponse
{
    public string senderEmail { get; set; }
    public string receiverEmail { get; set; }
    public double value { get; set; }
    public DateTime timeStamp { get; set; }

}

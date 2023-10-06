using System.Transactions;

public class DepositResponse
{
    public Guid? id { get; set; }
    public Guid? transaction { get; set; }
    public DateTime? timestamp { get; set; }
    public double? value { get; set; }
}
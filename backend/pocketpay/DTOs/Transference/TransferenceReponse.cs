public class TransferenceResponse
{
    public Guid id {get; set;}
    public Guid transaction {get; set;}
    public DateTime timestamp {get; set;}
    public string? sender {get; set;}
    public string? receiver {get; set;}
    public double value {get; set;}
}

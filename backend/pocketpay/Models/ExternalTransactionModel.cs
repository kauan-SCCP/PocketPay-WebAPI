
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ExternalTransaction")]
public class ExternalTransactionModel
{
    [Key]
    public Guid Id {get; set;}
    public DateTime Timestamp {get; set;}
    public double Value {get; set;}

    public AccountModel? Account {get; set;}
    public ExternalTransactionStatus Status {get; set;}
}
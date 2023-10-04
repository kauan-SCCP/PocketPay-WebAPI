using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pocketpay.Models;

[Table("Transferences")]
public class TransferenceModels
{
    [Key]
    public Guid Id { get; set; }
    public AccountModel Sender { get; set; }
    public AccountModel Receiver { get; set; }
    public TransactionModel Transaction { get; set; }
    public double Value { get; set; }
}

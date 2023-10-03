using pocketpay.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Deposit")]
public class DepositModel
{
    [Key]
    public Guid Id { get; set; }
    public TransactionModel? Transaction { get; set; }
    public AccountModel? Account { get; set; }
    public Double Value { get; set; }
}


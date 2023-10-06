using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pocketpay.Models
{
    [Table("Transaction")]
    public class TransactionModel
    {
        [Key]
        public Guid Id {get; set;}
        public DateTime TimeStamp {get; set;}
        public TransactionType Type {get; set;}
        public AccountModel? Owner {get; set;}
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pocketpay.Models
{
    [Table("Transaction")]
    public class TransactionModel
    {
        [Key]
        public Guid Id { get; set; }
        public AccountModel? From { get; set; }
        public AccountModel? To { get; set; }    
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
    }
}

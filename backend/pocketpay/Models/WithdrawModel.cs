using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pocketpay.Models
{
    [Table("Withdraw")]
    public class WithdrawModel
    {
        [Key]
        public Guid Id { get; set; }
        public TransactionModel? transaction { get; set; } 
        public AccountModel? Account { get; set; }   
        public double value { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pocketpay.Models
{
    [Table("Wallet")]
    public class WalletModel
    {
        public Guid Id { get; set; }
        public AccountModel Account { get; set; }   
        public double Balance { get; set; }
    }
}

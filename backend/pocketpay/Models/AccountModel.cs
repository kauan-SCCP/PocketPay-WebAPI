using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Account")]
public class AccountModel
{
    [Key]
    public Guid Id {get; set;}
    public string? Email {get; set;}
    public string? Password {get; set;}
    public string? Role {get; set;}
}
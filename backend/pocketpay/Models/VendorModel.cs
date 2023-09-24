
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Vendor")]
public class VendorModel
{
    [Key]
    public Guid Id {get; set;}
    public AccountModel? Account {get; set;}
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public string? CPNJ {get; set;}
}
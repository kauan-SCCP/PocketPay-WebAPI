
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Client")]
public class ClientModel
{
    [Key]
    public Guid Id {get; set;}
    public AccountModel? Account {get; set;}
    public string? Name {get; set;}
    public string? Surname {get; set;}
    public string? CPF {get; set;}

}
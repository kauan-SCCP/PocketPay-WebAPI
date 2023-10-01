
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Card")]
public class CardModel
{
    [Key]
    public Guid Id {get; set;}
    public AccountModel? Account {get; set;}
    public string? OwnerName {get; set;}
    public CardType Type {get; set;}
    public string? Number {get; set;}
    public string? CVV {get; set;}
    public string? ExpirationDate {get; set;}
    public bool IsActive {get; set;}
}
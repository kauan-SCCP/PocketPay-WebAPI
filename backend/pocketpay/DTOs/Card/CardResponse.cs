public class CardResponse
{
    public Guid id {get; set;}
    public string? owner {get; set;} 
    public CardType type {get; set;}
    public string? number {get; set;}
    public string? cvv {get; set;}
    public string? expiration_date {get; set;}
}
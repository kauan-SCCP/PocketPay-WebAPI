public class CardRegisterRequest
{
    public CardType type {get; set;}
    public string? number {get; set;}
    public string? cvv {get; set;}
    public string? expiration_date {get; set;}
    public string? owner {get; set;}
}
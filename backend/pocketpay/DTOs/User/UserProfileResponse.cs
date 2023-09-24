using Microsoft.AspNetCore.Authorization.Infrastructure;

public class UserProfileResponse
{
    public string? name {get; set;}
    public string? surname {get; set;}
    public string? email {get; set;}
    public string? cpf {get; set;}
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class TestController : ControllerBase
{
    [HttpGet("api/secret")]
    [Authorize]
    public String helloWorld()
    {
        return "Oi, usuário autorizado!";
    }
}
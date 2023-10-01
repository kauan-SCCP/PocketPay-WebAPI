using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/payment/card")]
public class CardController : ControllerBase
{
    private readonly ICardRepository cardRepository;
    private readonly IAccountRepository accountRepository;

    public CardController(ICardRepository cardRepository, IAccountRepository accountRepository)
    {
        this.cardRepository = cardRepository;
        this.accountRepository = accountRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCards()
    {
        var cards = await cardRepository.FindAll();
        
        var responseBody = new List<CardResponse>();

        foreach (CardModel card in cards)
        {
            if (card.IsActive) {
                var responseCard = new CardResponse()
                {
                    id = card.Id,
                    cvv = card.CVV,
                    expiration_date = card.ExpirationDate,
                    number = card.Number,
                    owner = card.OwnerName,
                    type = card.Type
                };

                responseBody.Add(responseCard);
            }
        }
        
        return Ok(responseBody);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCardById(Guid id)
    {
        var card = await cardRepository.FindById(id);

        if (card == null)
        {
            return NotFound();
        }

        var responseBody = new CardResponse()
        {
            id = card.Id,
            cvv = card.CVV,
            expiration_date = card.ExpirationDate,
            number = card.Number,
            owner = card.OwnerName,
            type = card.Type            
        };

        return Ok(responseBody);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> RegisterCard(CardRegisterRequest data)
    {
        
        if (User.Identity == null || User.Identity.Name == null)
        {
            return BadRequest();
        }

        var account = await accountRepository.FindByEmail(User.Identity.Name);

        if (account == null || data.owner == null || data.number == null || data.cvv == null || data.expiration_date == null)
        {
            return BadRequest();
        }
        
        var card = await cardRepository.Create(account, data.owner, data.type, data.number, data.cvv, data.expiration_date);
        
        var responsebody = new CardResponse()
        {
            id = card.Id,
            cvv = card.CVV,
            expiration_date = card.ExpirationDate,
            number = card.Number,
            owner = card.OwnerName,
            type = card.Type
        };
        
        return Ok(responsebody);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCard(Guid id)
    {
        var deletedCard = await cardRepository.Delete(id);

        if (deletedCard == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}

using Microsoft.EntityFrameworkCore;

public class CardRepository : ICardRepository
{
    private readonly BankContext _context;

    public CardRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<CardModel> Create(AccountModel account, string ownerName, CardType type, string number, string cvv, string expirationDate)
    {
        var card = new CardModel()
        {
            Id = new Guid(),
            Number = number,
            Type = type,
            CVV = cvv,
            ExpirationDate = expirationDate,
            Account = account,
            IsActive = true,
            OwnerName = ownerName
        };

        await _context.AddAsync(card);
        await _context.SaveChangesAsync();
        return card;
    }

    public async Task<CardModel?> Delete(Guid id)
    {
        var card = await FindById(id);

        if (card == null) {return null;}

        card.IsActive = false;
        await _context.SaveChangesAsync();

        return card;
    }

    public async Task<IEnumerable<CardModel>> FindActive()
    {
        return await _context.Cards
                .Include(card => card.Account)
                .Where(card => card.IsActive).ToListAsync();
    }

    public async Task<IEnumerable<CardModel>> FindAll()
    {
        return await _context.Cards.Include(card => card.Account).ToListAsync();
    }

    public async Task<CardModel?> FindById(Guid id)
    {
        return await _context.Cards.Include(card => card.Account).FirstOrDefaultAsync(card => card.Id == id);
    }

    public async Task<IEnumerable<CardModel>> FindUnactive()
    {
        return await _context.Cards
                        .Include(card => card.Account)
                        .Where(card => !card.IsActive).ToListAsync();
    }
}
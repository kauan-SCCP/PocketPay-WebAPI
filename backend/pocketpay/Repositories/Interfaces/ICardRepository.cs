public interface ICardRepository
{
    public Task<CardModel> Create(AccountModel account, string ownerName, CardType type, string number, string cvv, string expirationDate);
    public Task<CardModel?> FindById(Guid id);
    public Task<IEnumerable<CardModel>> FindActive();
    public Task<IEnumerable<CardModel>> FindUnactive();
    public Task<IEnumerable<CardModel>> FindAll();
    public Task<CardModel?> Delete(Guid id);
}
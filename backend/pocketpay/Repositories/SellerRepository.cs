using Microsoft.EntityFrameworkCore;

namespace pocketpay.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly BankContext _context;

        public SellerRepository(BankContext context)
        {
            _context = context; 
        }

        public async Task<SellerModel> Create(AccountModel account, string name, string surname, string cnpj)
        {
            var newSeller = new SellerModel();

            newSeller.Id = new Guid();
            newSeller.Account = account;
            newSeller.Name = name;
            newSeller.Surname = surname;
            newSeller.CNPJ = cnpj;

            await _context.AddAsync(newSeller);
            await _context.SaveChangesAsync();

            return newSeller;
        }

        public async Task<SellerModel?> FindById(Guid id)
        {
            var seller = await _context.Sellers
                .Include(seller => seller.Account)
                .FirstOrDefaultAsync(seller => seller.Id == id);

            return seller;
        }

        public async Task<SellerModel?> FindByCNPJ(string cnpj)
        {
            var seller = await _context.Sellers
                .Include(seller => seller.Account)
                .FirstOrDefaultAsync(seller => seller.CNPJ == cnpj);

            return seller;
        }

        public async Task<SellerModel?> FindByAccount(AccountModel account)
        {
            var seller = await _context.Sellers
                .Include(seller => seller.Account)
                .FirstOrDefaultAsync(seller => seller.Account == account);

            return seller;
        }

        public async Task<SellerModel?> Update(Guid id, AccountModel account, string name, string surname, string cnpj)
        {
            var seller = await FindById(id);

            if (seller == null) { return null;}

            seller.Account = account;
            seller.Name = name;
            seller.Surname = surname;
            seller.CNPJ = cnpj;
            await _context.SaveChangesAsync();

            return seller;
        }

        public async Task<SellerModel?> Delete(Guid id)
        {
            var seller = await FindById(id);
            if (seller == null) {return null;}

            _context.Remove(seller);
            await _context.SaveChangesAsync();

            return seller;
        }
    }
}

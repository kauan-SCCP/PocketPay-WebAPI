
using Microsoft.EntityFrameworkCore;

public class SellerRepository : ISellerRepository
{
    private readonly BankContext _context;

    public SellerRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<VendorModel> Create(string email, string password, string name, string surname, string cpnj)
    {
        var newAccount = new AccountModel();
        
        newAccount.Email = email;
        newAccount.Password = BCrypt.Net.BCrypt.HashPassword(password);
        newAccount.Role = "Seller";

        await _context.AddAsync(newAccount);
        await _context.SaveChangesAsync();

        var newVendor = new VendorModel();
        newVendor.Id = new Guid();
        newVendor.Account = newAccount;
        newVendor.Name = name;
        newVendor.Surname = surname;
        newVendor.CPNJ = cpnj;

        await _context.AddAsync(newVendor);
        await _context.SaveChangesAsync();

        return newVendor;
    }

    public async Task<VendorModel> GetById(Guid id)
    {
        var vendor = await _context.Vendors.FirstOrDefaultAsync( 
            account => account.Id == id
        );

        return vendor;
    }

    public async Task<VendorModel> GetByEmail(string email)
    {

        var vendor = await  _context.Vendors
            .Include(vendor => vendor.Account)
            .FirstOrDefaultAsync(vendor => vendor.Account.Email == email);

        return vendor;
    }

    public async Task<VendorModel> GetByCNPJ(string cpnj)
    {
        var vendor = await _context.Vendors.FirstOrDefaultAsync(
            vendor => vendor.CPNJ == cpnj
        );

        return vendor;
    }


    public async Task<VendorModel> Delete(Guid id)
    {
        var vendor = await _context.Vendors.FirstOrDefaultAsync( 
            account => account.Id == id
        );

        if (vendor == null) {return null;}

        _context.Remove(vendor);

        return vendor;
    }

    public async Task<VendorModel> UpdateById(Guid id, AccountModel account, string name, string surname, string cpnj)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) {return null;}

        vendor.Account = account;
        vendor.Name = name;
        vendor.Surname = surname;
        vendor.CPNJ = cpnj;

        await _context.SaveChangesAsync();
        return vendor;
    }
}
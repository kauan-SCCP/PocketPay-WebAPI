
using Microsoft.EntityFrameworkCore;

public class VendorRepository : IVendorRepository
{
    private readonly BankContext _context;

    public VendorRepository(BankContext context)
    {
        _context = context;
    
    }

    public async Task<VendorModel> Create(AccountModel account, string name, string surname, string cpnj)
    {
        var newVendor = new VendorModel();
        newVendor.Id = new Guid();
        newVendor.Account = account;
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

    public async Task<VendorModel> GetByEmail(String email)
    {

        var vendor = await  _context.Vendors
            .Include(vendor => vendor.Account)
            .FirstOrDefaultAsync(vendor => vendor.Account.Email == email);

        /*
        var vendor = await _context.Vendors.FirstOrDefaultAsync(
            vendor => vendor.Account.Email == email
        );
        */

        return vendor;
    }

    public async Task<VendorModel> GetByCNPJ(String cpnj)
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
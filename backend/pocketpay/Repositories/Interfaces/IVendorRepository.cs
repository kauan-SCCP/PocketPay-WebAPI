public interface IVendorRepository
{
    public Task<VendorModel> Create(AccountModel account, string name, string surname, string cpnj);
    public Task<VendorModel> GetById(Guid id);
    public Task<VendorModel> GetByEmail(String email);
    public  Task<VendorModel> GetByCNPJ(String cpnj);
    public Task<VendorModel> UpdateById(Guid id, AccountModel account, string name, string surname, string cpnj);
    public Task<VendorModel> Delete(Guid id);

}
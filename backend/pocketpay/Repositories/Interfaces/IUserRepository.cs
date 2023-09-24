public interface IUserRepository
{
    public Task<UserModel> createUser(UserRegisterRequest data);
    public  Task<AccountModel> checkCredentials(String email, String password);
}
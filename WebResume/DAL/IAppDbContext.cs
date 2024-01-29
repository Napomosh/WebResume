using WebResume.Model;

namespace WebResume.DAL;

public interface IAppDbContext{
    Task<UserModel> GetUser(string email);
    Task<UserModel> GetUser(int id);

    Task<int> SaveUser(UserModel userModel);
}
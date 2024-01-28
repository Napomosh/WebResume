using WebResume.Model;

namespace WebResume.DAL;

public interface IAppDbContext{
    Task<User> GetUser(string email);
    Task<User> GetUser(int id);

    Task<int> SaveUser(User user);
}
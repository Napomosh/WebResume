using WebResume.Model;

namespace WebResume.BL.Auth;

public interface IAuth{
    Task<int> Register(UserModel userModel);
    Task<bool> CheckRegistration(string? email, string? password);
    Task<bool> IsExistUser(string? email);
    Task<bool> IsExistUser(UserModel? user);
    Task Login(string email, string password, bool rememberMe);
}
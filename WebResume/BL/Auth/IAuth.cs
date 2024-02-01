namespace WebResume.BL.Auth;

public interface IAuth{
    Task<int> CreateUser(WebResume.Model.UserModel userModel);
    Task<bool> CheckRegistration(string? email, string? password);
    Task<int?> IsExistUser(string? email);
    Task<bool> Login(string email, string password);
}
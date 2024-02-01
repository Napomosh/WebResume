namespace WebResume.BL.Auth;

public interface IAuth{
    Task<int> CreateUser(WebResume.Model.UserModel userModel);
    void Login(int idUser);
    void Login(string email);
    Task<bool> CheckRegistration(string? email, string? password);
    Task<int?> IsExistUser(string? email);
}
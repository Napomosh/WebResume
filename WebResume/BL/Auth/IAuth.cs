namespace WebResume.BL.Auth;

public interface IAuth{
    Task<int> CreateUser(WebResume.Model.UserModel userModel);
    void Login(int idUser);
    void Login(string email);
    bool CheckRegistration(string? email, string? password);
    bool CheckRegistration(string? email);
}
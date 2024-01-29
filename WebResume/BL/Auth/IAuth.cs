namespace WebResume.BL.Auth;

public interface IAuth{
    Task<int> CreateUser(WebResume.Model.UserModel userModel);
}
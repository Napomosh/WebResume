namespace WebResume.BL.Auth;

public interface ICurrentUser{
    Task<bool> IsLoggedIn();
}
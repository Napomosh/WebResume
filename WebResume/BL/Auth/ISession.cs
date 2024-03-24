using WebResume.Model;

namespace WebResume.BL.Auth;

public interface ISession{
    Task<int> SetUserId(int id);
    Task<int?> GetUserId();
    Task<bool> IsLoggedIn();
    Task SetTokenId(Guid guid);
    Task<SessionModel> Get();
}
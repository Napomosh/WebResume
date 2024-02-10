namespace WebResume.BL.Auth;

public class CurrentUser(ISession session) : ICurrentUser{
    
    public async Task<bool> IsLoggedIn(){
        return await session.IsLoggedIn();
    }
}
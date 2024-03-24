using WebResume.BL.General;
using WebResume.DAL;
using WebResume.Utils;

namespace WebResume.BL.Auth;

public class CurrentUser(ISession session, IWebCookie webCookie, IDbUserToken userToken) : ICurrentUser{

    public async Task<int?> GetUserByToken(){
        string? cookieValue = webCookie.Get(AuthConstants.AUTH_REMEMBER_ME_COOKIE_NAME);
        Guid token = StringTools.StringToGuid(cookieValue);
        if (token.Equals(Guid.Empty))
            return null;
        return await userToken.GetUserId(token);
    }
    public async Task<bool> IsLoggedIn(){
        bool isLoggedIn = await session.IsLoggedIn();
        if (!isLoggedIn){
            int? userId = await GetUserByToken();
            if (userId != null){
                await session.SetUserId((int)userId);
                isLoggedIn = true;
            }
        }

        return isLoggedIn;
    }
}
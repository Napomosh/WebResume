namespace WebResume.BL.Auth;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public bool IsLoggedIn(){
        return _httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME) != null;
    }
}
using WebResume.DAL;
using WebResume.Model;

namespace WebResume.BL.Auth;

public class Session(IHttpContextAccessor httpContextAccessor, IDbSession dbSession): ISession{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IDbSession _dbSession = dbSession;
    private SessionModel? _sessionModel = null;

    public async Task<int> SetUserId(int id){
        var sessionObj = await GetSession();
        sessionObj.UserId = id;
        sessionObj.SessionId = Guid.NewGuid();  
        CreateSessionCookie(sessionObj.SessionId);
        await _dbSession.CreateSession(sessionObj);
        return id;
    }

    public async Task<SessionModel> GetSession(){
        if (_sessionModel != null) return _sessionModel;
        
        Guid sessionId;
        SessionModel? sessionObj;
        var cookie = _httpContextAccessor?.HttpContext?.Request.Cookies.FirstOrDefault
            (c => c.Key == AuthConstants.AUTH_SESSION_COOKIE_NAME);
        if(cookie != null && cookie.Value.Value != null){
            sessionId = Guid.Parse(cookie.Value.Value);
        }
        else{
            sessionObj = await CreateSession();
            CreateSessionCookie(sessionObj.SessionId);
            return sessionObj;
        }

        sessionObj = await _dbSession.GetSession(sessionId);
        if (sessionObj != null) return sessionObj;
        
        sessionObj = await CreateSession();
        CreateSessionCookie(sessionObj.SessionId);
        _sessionModel = sessionObj;
        return sessionObj;
    }
    
    public async Task<int?> GetUserId(){
        var sessionObj = await GetSession();
        return sessionObj?.UserId;
    }

    public async Task<bool> IsLoggedIn(){
        var sessionObj = await GetSession();
        return sessionObj?.UserId != 0;
    }
    
    private void CreateSessionCookie(Guid sessionId){
        CookieOptions options = new CookieOptions{
            Path = "/",
            HttpOnly = true,
            Secure = true
        };
        _httpContextAccessor?.HttpContext?.Response.Cookies.Delete(AuthConstants.AUTH_SESSION_COOKIE_NAME);
        _httpContextAccessor?.HttpContext?.Response.Cookies.Append(
            AuthConstants.AUTH_SESSION_COOKIE_NAME, sessionId.ToString(), options);
    }

    private async Task<SessionModel> CreateSession(){
        var newSession = new SessionModel(){
            SessionId = Guid.NewGuid(),
            CreatedDateTime = DateTime.Now,
            LastAccessedDateTime = DateTime.Now
        };
        await _dbSession.CreateSession(newSession);
        return newSession;
    }
}
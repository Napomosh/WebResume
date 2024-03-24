using WebResume.BL.General;
using WebResume.DAL;
using WebResume.Model;

namespace WebResume.BL.Auth;

public class Session(IHttpContextAccessor httpContextAccessor, IDbSession dbSession, IWebCookie webCookie): ISession{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IDbSession _dbSession = dbSession;
    private SessionModel? _sessionModel = null;

    public async Task<int> SetUserId(int id){
        var sessionObj = await Get();
        sessionObj.UserId = id;
        sessionObj.SessionId = Guid.NewGuid();
        sessionObj.UserTokenId = null;
        CreateSessionCookie(AuthConstants.AUTH_SESSION_COOKIE_NAME, sessionObj.SessionId, 30);
        await _dbSession.Create(sessionObj);
        return id;
    }

    public async Task<SessionModel> Get(){
        if (_sessionModel != null) return _sessionModel;
        
        Guid sessionId;
        SessionModel? sessionObj;
        string? cookie = webCookie.Get(AuthConstants.AUTH_SESSION_COOKIE_NAME);
        if(cookie != null)
            sessionId = Guid.Parse(cookie);
        else
            sessionId = Guid.NewGuid();
        
        sessionObj = await _dbSession.Get(sessionId);
        if (sessionObj == null){
            sessionObj = await Create();
            CreateSessionCookie(AuthConstants.AUTH_SESSION_COOKIE_NAME, sessionObj.SessionId, 30);
        }
        _sessionModel = sessionObj;
        return sessionObj;
    }
    
    public async Task<int?> GetUserId(){
        var sessionObj = await Get();
        return sessionObj?.UserId;
    }

    public async Task<bool> IsLoggedIn(){
        var sessionObj = await Get();
        return sessionObj?.UserId != 0;
    }

    public async Task SetTokenId(Guid guid){
        var sessionObj = await Get();
        sessionObj.UserTokenId = guid;
        CreateSessionCookie(AuthConstants.AUTH_REMEMBER_ME_COOKIE_NAME, guid, 100000000);
        await _dbSession.Create(sessionObj);
    }

    private void CreateSessionCookie(string name, Guid sessionId, int days = 0){
        webCookie.Delete(name);
        webCookie.AddSecure(name, sessionId.ToString(), days);
    }

    private async Task<SessionModel> Create(){
        var newSession = new SessionModel(){
            SessionId = Guid.NewGuid(),
            CreatedDateTime = DateTime.Now,
            LastAccessedDateTime = DateTime.Now
        };
        await _dbSession.Create(newSession);
        return newSession;
    }
}
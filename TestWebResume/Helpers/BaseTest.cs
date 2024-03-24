using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebResume.BL.Auth;
using WebResume.BL.General;
using WebResume.DAL;

namespace TestWebResume.Helpers;

public class BaseTest{
    protected readonly DbUser _db;
    protected readonly IEncrypt _enc = new Encrypt();
    protected readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
    protected readonly IAuth _auth;
    protected readonly IDbSession _dbSession;
    protected readonly WebResume.BL.Auth.ISession _session;
    protected IWebCookie _webCookie;
    protected IDbUserToken _dbUserToken;

    protected BaseTest(){
        var optionsBuilder = new DbContextOptionsBuilder<MsSqlAppDbContext>();
        optionsBuilder.UseSqlServer(Constants.CONNECTION_STRING);
        _webCookie = new TestCookie();
        _db = new DbUser(optionsBuilder.Options);
        _dbSession = new DbSession(optionsBuilder.Options);
        _dbUserToken = new DbUserToken(optionsBuilder.Options);
        _session = new Session(_httpContextAccessor, _dbSession, _webCookie);
        _auth = new Auth(_db, _enc, _session, _webCookie, _dbUserToken);
    }
}
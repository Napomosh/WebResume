using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebResume.BL.Auth;
using WebResume.DAL;
using ISession = WebResume.BL.Auth.ISession;

namespace TestWebResume.Helpers;

public class BaseTest{
    protected readonly DbUser _db;
    protected readonly IEncrypt _enc = new Encrypt();
    protected readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
    protected readonly IAuth _auth;
    protected readonly IDbSession _dbSession;
    protected readonly ISession _session;

    protected BaseTest(){
        var optionsBuilder = new DbContextOptionsBuilder<MsSqlAppDbContext>();
        optionsBuilder.UseSqlServer(Constants.CONNECTION_STRING);
        _db = new DbUser(optionsBuilder.Options);
        _dbSession = new DbSession(optionsBuilder.Options);
        //_session = new Session(_httpContextAccessor, _dbSession);
        //_auth = new Auth(_db, _enc, _session);
    }
}
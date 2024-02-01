using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebResume.BL.Auth;
using WebResume.DAL;

namespace TestWebResume.Helpers;

public class BaseTest{
    protected readonly MsSqlAppDbContext _db;
    protected readonly IEncrypt _enc = new Encrypt();
    protected readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
    protected readonly IAuth _auth; 

    protected BaseTest(){
        var optionsBuilder = new DbContextOptionsBuilder<MsSqlAppDbContext>();
        optionsBuilder.UseSqlServer(Constants.CONNECTION_STRING);
        _db = new MsSqlAppDbContext(optionsBuilder.Options);
        
        _auth = new Auth(_db, _enc, _httpContextAccessor);
    }
}
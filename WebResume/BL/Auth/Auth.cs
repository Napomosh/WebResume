using WebResume.DAL;

namespace WebResume.BL.Auth;

public class Auth(MsSqlAppDbContext db, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor) : IAuth{
    private readonly MsSqlAppDbContext _db = db;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public async Task<int> CreateUser(WebResume.Model.UserModel userModel){
        HashingPassword(userModel);
        var res = await _db.SaveUser(userModel);
        await _db.SaveChangesAsync();
        Login(res);
        return res;
    }

    public void Login(int id){
        _httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
    }

    private void HashingPassword(WebResume.Model.UserModel userModel){
        userModel.Salt = Guid.NewGuid().ToString();
        userModel.Password = encrypt.HashPassword(userModel.Password, userModel.Salt);
    }
}
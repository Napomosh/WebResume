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

    public async void Login(string email){
        var user = await _db.GetUser(email);
        Login(user.UserId);
    }

    private void HashingPassword(WebResume.Model.UserModel userModel){
        userModel.Salt = Guid.NewGuid().ToString();
        userModel.Password = HashingPassword(userModel.Password, userModel.Salt);
    }
    
    private string HashingPassword(string password, string salt){
        return encrypt.HashPassword(password, salt);
    }

    public async Task<bool> CheckRegistration(string? email, string? password){
        if (email == null || password == null)
            return false;
        var user = await _db.GetUser(email);
        return HashingPassword(password, user.Salt).Equals(user.Password);
    }

    public async Task<bool> IsExistUser(string? email){
        if (email == null)
            return false;
        var user = await _db.GetUser(email);
        return user.UserId != 0;
    }
}
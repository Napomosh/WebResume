using WebResume.BL.Exception;
using WebResume.BL.General;
using WebResume.DAL;
using WebResume.Model;

namespace WebResume.BL.Auth;

public class Auth(IDbUser dbUser, IEncrypt encrypt, ISession session, IWebCookie webCookie,
    IDbUserToken dbUserToken) : IAuth{
    
    private readonly IDbUser _dbUser = dbUser;
    private readonly IDbUserToken _dbUserToken = dbUserToken;

    public async Task<int> Register(UserModel userModel){
        if (IsExistUser(userModel).Result)
            throw new DuplicateEmailException();
        var res = await CreateUser(userModel);
        return res;
    }
    
    public async Task<bool> CheckRegistration(string? email, string? password){
        if (email == null || password == null)
            return false;
        var user = await _dbUser.GetUser(email);
        return HashingPassword(password, user.Salt).Equals(user.Password);
    }

    public bool CheckRegistration(UserModel? user, string? password){
        if (user == null || password == null)
            return false;
        return HashingPassword(password, user.Salt).Equals(user.Password);
    }
    
    public async Task<bool> IsExistUser(string? email){
        if (email == null)
            return false;
        // TODO Reduce accesses to DB
        // 1. we get user from DB (1 access)
        // 2. we call check exist user (2 access)
        var user = await _dbUser.GetUser(email);
        return user.UserId != 0 ? true : false;
    }
    public async Task<bool> IsExistUser(UserModel? user){
        if (user == null)
            return false;
       return await IsExistUser(user.Email);
    }

    public async Task Login(string email, string password, bool rememberMe ){
        var user = await _dbUser.GetUser(email);
        if (!IsExistUser(user).Result || !CheckRegistration(user, password)) 
            throw new AuthorizationException();

        if (rememberMe){
            Guid tokenGuid = await _dbUserToken.Create(user.UserId);
            webCookie.AddSecure(AuthConstants.AUTH_REMEMBER_ME_COOKIE_NAME, tokenGuid.ToString(), 120);
        }
        await session.SetUserId(user.UserId); 
    }
    
    private async Task<int> CreateUser(UserModel userModel){
        HashingPassword(userModel);
        var res = await _dbUser.SaveUser(userModel);
        await session.SetUserId(res);
        return res;
    }
    
    private void HashingPassword(UserModel userModel){
        userModel.Salt = Guid.NewGuid().ToString();
        userModel.Password = HashingPassword(userModel.Password, userModel.Salt);
    }
    
    private string HashingPassword(string password, string salt){
        return encrypt.HashPassword(password, salt);
    }
}
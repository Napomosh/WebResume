using WebResume.BL.Exception;
using WebResume.DAL;
using WebResume.Model;

namespace WebResume.BL.Auth;

public class Auth(IDbUser dbUserUser, IEncrypt encrypt, ISession session) : IAuth{
    private readonly IDbUser _dbUser = dbUserUser;

    public async Task<int> Register(UserModel userModel){
        await session.Lock();
        if (await IsExistUser(userModel.Email))
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

    public async Task<bool> IsExistUser(string? email){
        if (email == null)
            return false;
        var user = await _dbUser.GetUser(email);
        return user?.UserId != 0;
    }

    public async Task Login(string email, string password){
        if (!await IsExistUser(email) || !await CheckRegistration(email, password)) 
            throw new AuthorizationException();
        
        WriteUserIdInSession(email);
    }

    private async void WriteUserIdInSession(string email){
        var user = await _dbUser.GetUser(email);
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
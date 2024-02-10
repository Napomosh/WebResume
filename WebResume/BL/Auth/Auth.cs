using WebResume.DAL;

namespace WebResume.BL.Auth;

public class Auth(IDbUser dbUserUser, IEncrypt encrypt, ISession session) : IAuth{
    private readonly IDbUser _dbUser = dbUserUser;
    
    public async Task<int> CreateUser(WebResume.Model.UserModel userModel){
        HashingPassword(userModel);
        var res = await _dbUser.SaveUser(userModel);
        await session.SetUserId(res);
        return res;
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
        var user = await _dbUser.GetUser(email);
        return HashingPassword(password, user.Salt).Equals(user.Password);
    }

    public async Task<int?> IsExistUser(string? email){
        if (email == null)
            return null;
        var user = await _dbUser.GetUser(email);
        return user.UserId != 0 ? user.UserId : null;
    }

    public async Task<bool> Login(string email, string password){
        var isUserExist = await IsExistUser(email);
        if (isUserExist == null)
            return false;
        var isUserReg = await CheckRegistration(email, password);
        if (!isUserReg)
            return false;
        
        WriteUserIdInSession(email);
        return true; 
    }

    private async void WriteUserIdInSession(string email){
        var user = await _dbUser.GetUser(email);
        await session.SetUserId(user.UserId);
    }
}
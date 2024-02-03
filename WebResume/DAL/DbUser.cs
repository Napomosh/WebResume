using Microsoft.EntityFrameworkCore;
using WebResume.Model;

namespace WebResume.DAL;

public class DbUser(DbContextOptions<MsSqlAppDbContext> options) : MsSqlAppDbContext(options), IDbUser{
    public async Task<UserModel> GetUser(string email){
        return await Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync() ?? new UserModel();
    }

    public async Task<UserModel> GetUser(int id){
        return await Users.Where(u => u.UserId == id).FirstOrDefaultAsync() ?? new UserModel();
    }

    public async Task<int> SaveUser(UserModel userModel){
        var query = await Users.AddAsync(userModel);
        await SaveChangesAsync();
        return userModel.UserId;
    }
}
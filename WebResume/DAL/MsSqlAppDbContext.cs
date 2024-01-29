using Microsoft.EntityFrameworkCore;
using WebResume.Model;

namespace WebResume.DAL;

public class MsSqlAppDbContext : DbContext, IAppDbContext{
    public MsSqlAppDbContext(DbContextOptions<MsSqlAppDbContext> options) : base(options){
        
    }
    public DbSet<UserModel> Users{ get; init; }


    public async Task<UserModel> GetUser(string email){
        return await Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync() ?? new UserModel();
    }

    public async Task<UserModel> GetUser(int id){
        return await Users.Where(u => u.UserId == id).FirstOrDefaultAsync() ?? new UserModel();
    }

    public async Task<int> SaveUser(UserModel userModel){
        var query = await Users.AddAsync(userModel);
        return query.Entity.UserId;
    }

    public async Task<bool> CheckUserRegistration(string email, string password){
        return await Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password)).AnyAsync();
    }
}
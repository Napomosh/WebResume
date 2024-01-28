using Microsoft.EntityFrameworkCore;
using WebResume.Model;

namespace WebResume.DAL;

public class MsSqlAppDbContext : DbContext, IAppDbContext{
    public MsSqlAppDbContext(DbContextOptions<MsSqlAppDbContext> options) : base(options){
        
    }
    public DbSet<User> Users{ get; init; }


    public async Task<User> GetUser(string email){
        return await Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync() ?? new User();
    }

    public async Task<User> GetUser(int id){
        return await Users.Where(u => u.UserId == id).FirstOrDefaultAsync() ?? new User();
    }

    public async Task<int> SaveUser(User user){
        var query = await Users.AddAsync(user);
        return query.GetDatabaseValuesAsync().Id;
    }
}
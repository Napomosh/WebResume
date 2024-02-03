using Microsoft.EntityFrameworkCore;
using WebResume.Model;

namespace WebResume.DAL;

public class MsSqlAppDbContext : DbContext, IAppDbContext{
    public MsSqlAppDbContext(DbContextOptions<MsSqlAppDbContext> options) : base(options){
        
    }
    protected DbSet<UserModel> Users{ get; init; }
    protected DbSet<ProfileModel> Profiles{ get; init; }
    protected DbSet<SessionModel> UserSessions{ get; init; }
}
using Microsoft.EntityFrameworkCore;
using WebResume.Model;

namespace WebResume.DAL;

public class DbUserToken(DbContextOptions<MsSqlAppDbContext> options) : MsSqlAppDbContext(options),  IDbUserToken{
    public async Task<Guid> Create(int userId){
        Guid guid = Guid.NewGuid();
        UserTokenModel newUserToken = new UserTokenModel{
            TokenId = guid,
            UserId = userId,
            CreatedTime = DateTime.Now,
        };
        await UserTokens.AddAsync(newUserToken);
        await SaveChangesAsync();
        return guid;
    }

    public async Task<int?> GetUserId(Guid id){
        var user = await UserTokens.Where(x => x.TokenId == id).FirstOrDefaultAsync();
        return user?.UserId;
    }
}
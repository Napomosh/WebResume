using WebResume.Model;

namespace WebResume.DAL;

public interface IDbUserToken{
    public Task<Guid> Create(int userId);
    public Task<int?> GetUserId(Guid id);
}
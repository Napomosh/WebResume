using WebResume.Model;

namespace WebResume.DAL;

public interface IDbSession{
    Task<SessionModel?> Get(Guid sessionId);
    Task<Guid> Create(SessionModel session);
    Task<int> Update(SessionModel session);
    Task Lock(Guid sessionId);
}
using WebResume.Model;

namespace WebResume.DAL;

public interface IDbSession{
    Task<SessionModel?> GetSession(Guid sessionId);
    Task<Guid> CreateSession(SessionModel session);
    Task<int> UpdateSession(SessionModel session);
}
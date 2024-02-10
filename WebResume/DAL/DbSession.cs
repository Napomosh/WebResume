using Microsoft.EntityFrameworkCore;
using WebResume.BL.Auth;
using WebResume.Model;

namespace WebResume.DAL;

public class DbSession(DbContextOptions<MsSqlAppDbContext> options) : MsSqlAppDbContext(options), IDbSession{
    public async Task<SessionModel?> GetSession(Guid sessionId){
        var sessionRes = await UserSessions.Where(s => s.SessionId == sessionId).FirstOrDefaultAsync();
        return sessionRes;
    }
    
    public async Task<Guid> CreateSession(SessionModel session){
        var creationRes = await UserSessions.AddAsync(session);
        await SaveChangesAsync();
        return session.SessionId;
    }
    
    public async Task<int> UpdateSession(SessionModel session){
        var tmpSessionObj = await UserSessions.Where(s => s.SessionId == session.SessionId).FirstOrDefaultAsync();
        if (tmpSessionObj != null)
            UpdateSessionFields(session, tmpSessionObj);
        return await SaveChangesAsync();
    }

    private static void UpdateSessionFields(SessionModel session, SessionModel tmpSession){
        tmpSession.SessionData = session.SessionData;
        tmpSession.CreatedDateTime = session.CreatedDateTime;
        tmpSession.LastAccessedDateTime = session.LastAccessedDateTime;
    }
}
using Microsoft.EntityFrameworkCore;
using WebResume.BL.Auth;
using WebResume.Model;

namespace WebResume.DAL;

public class DbSession(DbContextOptions<MsSqlAppDbContext> options) : MsSqlAppDbContext(options), IDbSession{
    public async Task<SessionModel?> Get(Guid sessionId){
        var sessionRes = await UserSessions.Where(s => s.SessionId == sessionId).FirstOrDefaultAsync();
        return sessionRes;
    }
    
    public async Task<Guid> Create(SessionModel session){
        var creationRes = await UserSessions.AddAsync(session);
        await SaveChangesAsync();
        return session.SessionId;
    }
    
    public async Task<int> Update(SessionModel session){
        var tmpSessionObj = await UserSessions.Where(s => s.SessionId == session.SessionId).FirstOrDefaultAsync();
        if (tmpSessionObj != null)
            UpdateFields(session, tmpSessionObj);
        return await SaveChangesAsync();
    }

    private static void UpdateFields(SessionModel session, SessionModel tmpSession){
        tmpSession.SessionData = session.SessionData;
        tmpSession.CreatedDateTime = session.CreatedDateTime;
        tmpSession.LastAccessedDateTime = session.LastAccessedDateTime;
    }
}
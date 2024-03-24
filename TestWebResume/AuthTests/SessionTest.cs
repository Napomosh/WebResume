using System.Transactions;
using TestWebResume.Helpers;
using WebResume.Model;

namespace TestWebResume.AuthTests;

public class SessionTest : BaseTest{
    [SetUp]
    public void Setup(){
        
    }

    [Test]
    public async Task TestSession(){
        using TransactionScope scope = Helper.CreateTransactionScope();
        
        SessionModel sessionObj = await _session.Get();
        Assert.IsNotNull(sessionObj);
        
        var dbSession = await _dbSession.Get(sessionObj.SessionId);
        Assert.IsNotNull(dbSession);
        Assert.That(dbSession?.SessionId, Is.EqualTo(sessionObj.SessionId));
        
        SessionModel sessionObj1 = await _session.Get();
        Assert.IsNotNull(sessionObj1);
        Assert.That(dbSession?.SessionId, Is.EqualTo(sessionObj1.SessionId));
    }
}
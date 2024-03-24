using System.Transactions;
using TestWebResume.Helpers;
using WebResume.BL.Exception;
using WebResume.Model;

namespace TestWebResume.AuthTests;

public class LoginTest : BaseTest{
    [SetUp]
    public void Setup(){
    }

    [Test]
    public async Task BaseLoginTest(){
        using (TransactionScope scope = Helper.CreateTransactionScope()){
            string testEmail = Guid.NewGuid().ToString() + "@test.com";
            string testPassword = "test";
            
            Assert.Throws<AuthorizationException>(delegate { _auth.Login(testEmail, testPassword, false).GetAwaiter().GetResult(); });

            await _auth.Register(new UserModel{
                Email = testEmail,
                Password = testPassword
            });
            
            Assert.DoesNotThrow(delegate { _auth.Login(testEmail, testPassword, true).GetAwaiter().GetResult(); });
            
            string testPasswordNew = Guid.NewGuid().ToString();
            Assert.Throws<AuthorizationException>(delegate { _auth.Login(testEmail, testPasswordNew, true).GetAwaiter().GetResult(); });
            
            string testEmailNew = Guid.NewGuid().ToString() + "@test.com";
            Assert.Throws<AuthorizationException>(delegate { _auth.Login(testEmailNew, testPassword, true).GetAwaiter().GetResult(); });
            
            Assert.Throws<AuthorizationException>(delegate { _auth.Login(testEmailNew, testPasswordNew, true).GetAwaiter().GetResult(); });
        }
    }
}
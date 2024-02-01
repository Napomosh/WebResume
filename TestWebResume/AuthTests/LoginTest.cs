using System.Transactions;
using TestWebResume.Helpers;
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

            var resLogin = await _auth.Login(testEmail, testPassword);
            Assert.IsFalse(resLogin);

            await _auth.CreateUser(new UserModel{
                Email = testEmail,
                Password = testPassword
            });
            
            resLogin = await _auth.Login(testEmail, testPassword);
            Assert.IsTrue(resLogin);
            
            string testPasswordNew = Guid.NewGuid().ToString();
            resLogin = await _auth.Login(testEmail, testPasswordNew);
            Assert.IsFalse(resLogin);
            
            string testEmailNew = Guid.NewGuid().ToString() + "@test.com";
            resLogin = await _auth.Login(testEmailNew, testPassword);
            Assert.IsFalse(resLogin);
        
            resLogin = await _auth.Login(testEmailNew, testPasswordNew);
            Assert.IsFalse(resLogin);
        }
    }
}
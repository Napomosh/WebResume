using System.Transactions;
using TestWebResume.Helpers;
using WebResume.BL.Exception;
using WebResume.Model;

namespace TestWebResume.AuthTests;

public class RegisterTest : BaseTest{
    [SetUp]
    public void Setup(){
    }

    [Test]
    public async Task BaseRegistrationTest(){
        using (TransactionScope scope = Helper.CreateTransactionScope()){
            string testEmail = Guid.NewGuid().ToString() + "@test.com";
            var resEmailChecking = await _auth.IsExistUser(testEmail);
            Assert.IsFalse(resEmailChecking);

            var userId = await _auth.Register(new UserModel{
                Email = testEmail,
                Password = "test"
            });
            
            Assert.Greater(userId, 0);
            
            var resEmailRegister = await _auth.CheckRegistration(testEmail, "test");
            Assert.True(resEmailRegister);
            
            resEmailChecking = await _auth.IsExistUser(testEmail);
            Assert.IsNotNull(resEmailChecking);

            Assert.Throws<DuplicateEmailException>(delegate{
                _auth.Register(new UserModel{
                    Email = testEmail,
                    Password = "test"
                }).GetAwaiter().GetResult();
            });
        }
    }
}
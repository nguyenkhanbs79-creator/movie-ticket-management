using Cinema.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cinema.Tests.Security
{
    [TestClass]
    public class PasswordHasherTests
    {
        [TestMethod]
        public void HashPassword_And_Verify_ShouldSucceed()
        {
            var hasher = new PasswordHasher();
            var credential = hasher.HashPassword("MyStrongPassword!123");

            Assert.IsTrue(hasher.Verify("MyStrongPassword!123", credential));
        }

        [TestMethod]
        public void Verify_ShouldFail_ForWrongPassword()
        {
            var hasher = new PasswordHasher();
            var credential = hasher.HashPassword("AnotherPassword");

            Assert.IsFalse(hasher.Verify("WrongPassword", credential));
        }
    }
}

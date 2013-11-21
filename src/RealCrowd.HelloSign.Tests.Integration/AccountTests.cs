using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using RealCrowd.HelloSign;
using RealCrowd.HelloSign.Models;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public async Task SetCallbackTest()
        {
            HelloSignClient client = new HelloSignClient(Config.Username, Config.Password);

            Account account = await client.Account.Get();
            Assert.IsTrue(account.EmailAddress == Config.Username);

            Account accountUpdate = await client.Account.Update(new AccountUpdateRequest { CallbackUrl = "http://test" });
            Assert.IsTrue(accountUpdate.CallbackUrl == "http://test");

            Account accountUpdateDeleteCallback = await client.Account.Update(new AccountUpdateRequest { CallbackUrl = "" });
            Assert.IsTrue(accountUpdateDeleteCallback.CallbackUrl == null);
        }
    }
}

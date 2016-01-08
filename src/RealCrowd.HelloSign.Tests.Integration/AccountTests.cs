// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

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
        HelloSignClient client;

        [TestInitialize]
        public void Init()
        {
            if (!string.IsNullOrEmpty(Config.ApiKey))
            {
                client = new HelloSignClient(Config.ApiKey);
            }
            else if (!string.IsNullOrEmpty(Config.Username))
            {
                client = new HelloSignClient(Config.Username, Config.Password);
            }
            else
            {
                Assert.Fail("Config.Username or Config.ApiKey must be set to run test. Environment variables are suggested");
            }
            
        }

        [TestMethod]
        public async Task GetAccountTest()
        {
            Account account = await client.Account.GetAsync();

            Assert.IsNotNull(account);
            if (string.IsNullOrEmpty(Config.Username))
            {
                Assert.Inconclusive("Config.Username must be set to run test correctly.");
            }
            else
            {
                Assert.AreEqual(Config.Username, account.EmailAddress);
                Assert.AreNotEqual(string.Empty, account.AccountId);
                Assert.AreNotEqual(string.Empty, account.RoleCode);
                if (account.RoleCode != AccountRoleCodes.Admin && account.RoleCode != AccountRoleCodes.Member)
                {
                    Assert.Inconclusive("Unexpective RoleCode: {0}", account.RoleCode);
                }
                Assert.IsNotNull(account.Quotas);

                // dependent on account being tested
                //Assert.IsTrue(account.Quotas.ApiSignatureRequestsLeft > 0);
                //Assert.IsTrue(account.Quotas.ApiTemplatesLeft > 0);
                //Assert.IsTrue(account.Quotas.DocumentsLeft > 0);
                //Assert.AreNotEqual(false, account.IsPaidHelloFaxAccount);
                //Assert.AreNotEqual(false, account.IsPaidHelloSignAccount);
            }
        }

        [TestMethod]
        public async Task UpdateAccountTest()
        {
            Account accountUpdate = await client.Account.UpdateAsync(new AccountUpdateRequest { CallbackUrl = "http://test.com" });
            Assert.IsTrue(accountUpdate.CallbackUrl == "http://test.com");

            Account accountUpdateDeleteCallback = await client.Account.UpdateAsync(new AccountUpdateRequest { CallbackUrl = "" });
            Assert.IsTrue(accountUpdateDeleteCallback.CallbackUrl == null);
        }
    }
}

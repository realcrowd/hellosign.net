using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using RealCrowd.HelloSign;
using RealCrowd.HelloSign.Models;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class ReusableFormTests
    {
        [TestMethod]
        public async Task ListReusableFormsTest()
        {
            HelloSignClient client = new HelloSignClient(Config.Username, Config.Password);

            ReusableFormList list = await client.ReusableForm.ListAsync();
            Assert.IsTrue(list.ListInfo.NumResults > 0);
        }

        [TestMethod]
        public async Task GetReusableFormTest()
        {

        }

        [TestMethod]
        public async Task AddUserToReusableFormTest()
        {

        }

        [TestMethod]
        public async Task RemoveUserFromReusableFormTest()
        {

        }
    }
}

// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

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
        HelloSignClient client;

        [TestInitialize]
        public void Init()
        {
            client = new HelloSignClient(Config.Username, Config.Password);
        }

        [TestMethod]
        public async Task ListReusableFormsTest()
        {
            ReusableFormList list = await client.ReusableForm.ListAsync();
            Assert.IsTrue(list.ListInfo.NumResults > 0);
        }

        [TestMethod]
        public void GetReusableFormTest()
        {

        }

        [TestMethod]
        public void AddUserToReusableFormTest()
        {

        }

        [TestMethod]
        public void RemoveUserFromReusableFormTest()
        {

        }
    }
}

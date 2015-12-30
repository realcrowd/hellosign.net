// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class UnclaimedDraftTests
    {
        HelloSignClient client;

        [TestInitialize]
        public void Init()
        {
            if (!string.IsNullOrEmpty(Config.ApiKey))
            {
                client = new HelloSignClient(Config.ApiKey);
            }
            else
            {
                client = new HelloSignClient(Config.Username, Config.Password);
            }
        }

        [TestMethod]
        public void CreateUnclaimedDraftTest()
        {

        }
    }
}

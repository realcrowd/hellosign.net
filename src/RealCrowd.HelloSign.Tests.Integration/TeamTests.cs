// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using RealCrowd.HelloSign;
using RealCrowd.HelloSign.Models;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class TeamTests
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
        public async Task GetTeamTest()
        {
            Team team = await client.Team.GetAsync();

            Assert.IsTrue(!string.IsNullOrEmpty(team.Name));
        }

        [TestMethod]
        public void CreateTeamTest()
        {

        }

        [TestMethod]
        public void UpdateTeamTest()
        {

        }

        [TestMethod]
        public void DestoryTeamTest()
        {

        }

        [TestMethod]
        public void AddMemberTest()
        {

        }

        [TestMethod]
        public void RemoveMemberTest()
        {

        }
    }
}

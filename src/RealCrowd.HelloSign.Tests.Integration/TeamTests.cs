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
        [TestMethod]
        public async Task GetTeamTest()
        {
            HelloSignClient client = new HelloSignClient(Config.Username, Config.Password);

            Team team = await client.Team.GetAsync();

            Assert.IsTrue(!string.IsNullOrEmpty(team.Name));
        }

        [TestMethod]
        public async Task CreateTeamTest()
        {

        }

        [TestMethod]
        public async Task UpdateTeamTest()
        {

        }

        [TestMethod]
        public async Task DestoryTeamTest()
        {

        }

        [TestMethod]
        public async Task AddMemberTest()
        {

        }

        [TestMethod]
        public async Task RemoveMemberTest()
        {

        }
    }
}

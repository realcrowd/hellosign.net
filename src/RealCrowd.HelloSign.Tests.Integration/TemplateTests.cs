// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using System;
using System.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using RealCrowd.HelloSign;
using RealCrowd.HelloSign.Models;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class TemplateTests
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
        public async Task GetTemplateList()
        {
            TemplateListResponse templateList = await client.Template.GetListAsync(null);
            Assert.IsNotNull(templateList);
        }

        [TestMethod]
        public async Task GetTemplate()
        {
            Template template = await client.Template.GetAsync(new TemplateRequest(){TemplateId = Config.SimpleUnorderedTemplateId});
            Assert.AreEqual(Config.SimpleUnorderedTemplateId,template.TemplateId);
        }
    }
}

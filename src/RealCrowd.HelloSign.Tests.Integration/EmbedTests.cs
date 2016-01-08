using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealCrowd.HelloSign.Models;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class EmbeddedTests
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
        public async Task CreateEmbeddedSignRequestFromTemplate()
        {
            var embeddedSignRequest = new EmbeddedSignatureFromTemplateRequest()
            {
                TestMode = 1,
                TemplateId = Config.TemplateId1,
                Title = "Test Signature Request",
                Subject = "test subject",
                Message = "test message",
                ClientId = Config.ClientId,
                Signers = new Dictionary<string, SignatureRequestSignerRoleRequest>()
                {
                    {
                        "TestRole1",
                        new SignatureRequestSignerRoleRequest
                        {
                            EmailAddress = "test@test.com",
                            Name = "Test Test",
                        }
                    },
                    {
                        "TestRole2",
                        new SignatureRequestSignerRoleRequest
                        {
                            EmailAddress = "test2@test.com",
                            Name = "Test Test",
                        }
                    }
                },
                CustomFields = new Dictionary<string, string>{{ "TestCustomSendingField1", "Bob"}}
            };
            var signatureResponse = await client.SignatureRequest.SendEmbeddedWithTemplateAsync(embeddedSignRequest);
            
            Assert.AreEqual(signatureResponse.Title,embeddedSignRequest.Title);
            Assert.AreEqual(signatureResponse.Signatures.Count, 2);

            var signatureUrlResponse =
                await client.Embedded.GetSignUrlAsync(signatureResponse.Signatures.First().SignatureId);
            Assert.IsFalse(string.IsNullOrWhiteSpace(signatureUrlResponse.SignUrl));
        }
    }
}

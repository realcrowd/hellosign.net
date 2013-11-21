using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using RealCrowd.HelloSign;
using RealCrowd.HelloSign.Models;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class SignatureRequestTests
    {
        [TestMethod]
        public async Task CreateAndSendSignatureRequestTest()
        {
            HelloSignClient client = new HelloSignClient(Config.Username, Config.Password);
            
            SignatureRequestSendRequest sendRequest = new SignatureRequestSendRequest
            {
                TestMode = 1,
                Title = "Test Signature Request",
                Subject = "test subject",
                Message = "test message",
                Signers = new List<SignerRequest>() 
                {
                    new SignerRequest 
                    {  
                        EmailAddress = "test@test.com",
                        Name = "Test Test",
                        Order = 0
                    }
                },
                CcEmailAddresses = new List<string>()
                {
                    "test@test.com"
                },
                Files = new List<string>() 
                { 
                    "@test.pdf"
                }
            };

            SignatureRequest signatureRequest = await client.SignatureRequest.SendAsync(sendRequest);

            Assert.IsTrue(signatureRequest.Title == sendRequest.Title);
            Assert.IsTrue(signatureRequest.Subject == sendRequest.Subject);
            Assert.IsTrue(signatureRequest.Message == sendRequest.Message);
            Assert.IsTrue(signatureRequest.CcEmailAddresses[0] == sendRequest.CcEmailAddresses[0]);
        }

        [TestMethod]
        public async Task ListSignatureRequestsTest()
        {
            HelloSignClient client = new HelloSignClient(Config.Username, Config.Password);
            SignatureRequestList list = await client.SignatureRequest.ListAsync();
            Assert.IsTrue(list.ListInfo.NumResults > 0);
        }

        [TestMethod]
        public async Task GetSignatureRequestTest()
        {

        }

        public async Task SendWithReusableFormTest()
        {
            
        }

        public async Task RemindSignatureRequestTest()
        {
            
        }

        public async Task CancelSignatureRequestTest()
        {

        }

        public async Task FinalCopySignautreRequestTest()
        {

        }
    }
}

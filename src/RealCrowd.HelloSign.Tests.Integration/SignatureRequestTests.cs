// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using RealCrowd.HelloSign;
using RealCrowd.HelloSign.Models;
using System.IO;
using System.Net;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class SignatureRequestTests
    {
        HelloSignClient client;
        
        [TestInitialize]
        public void Init()
        {
            client = new HelloSignClient(Config.Username, Config.Password);
        }

        [TestMethod]
        public async Task CreateAndSendSignatureRequestTest()
        {
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

        [TestMethod]
        public async Task SendWithReusableFormTest()
        {
            SignatureRequestSendReusableFormRequest sendRequest = new SignatureRequestSendReusableFormRequest
            {
                TestMode = 1,
                ReusableFormId = "d95e188615fae3fab2fd717ed1190d42083b81f1",
                Title = "Test Title",
                Subject = "Test Subject",
                Message = "Test Message",
                Signers = new Dictionary<string, SignatureRequestSignerRequest>()
                {
                    { "Test", new SignatureRequestSignerRequest { Name = "Ross Stovall", EmailAddress = "ross@realcrowd.com" } },
                    { "Test 2", new SignatureRequestSignerRequest { Name = "Ross Stovall", EmailAddress = "rossco@gmail.com" } }
                }
            };

            SignatureRequest signatureRequest = await client.SignatureRequest.SendWithReusableFormAsync(sendRequest);

            Assert.IsTrue(signatureRequest.Title == sendRequest.Title);
            Assert.IsTrue(signatureRequest.Subject == sendRequest.Subject);
            Assert.IsTrue(signatureRequest.Message == sendRequest.Message);
        }

        public async Task RemindSignatureRequestTest()
        {
            
        }

        public async Task CancelSignatureRequestTest()
        {

        }

        [TestMethod]
        public async Task FinalCopySignautreRequestTest()
        {
            string signatureRequestId = "9556db0a06f1f7e8227820561f6a57652bb64c96";

            Func<Stream, Task> onStreamAvailable = async (outputStream) =>
            {
                string filePath = @"C:\test\test.pdf";
                byte[] buffer = new byte[32768];
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    int bytesRead;
                    while ((bytesRead = outputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                    }
                }
            };

            SignatureRequestFinalCopyRequest request = new SignatureRequestFinalCopyRequest(onStreamAvailable)
            {
                SignatureRequestId = signatureRequestId
            };

            await client.SignatureRequest.FinalCopyAsync(request);
        }
    }
}

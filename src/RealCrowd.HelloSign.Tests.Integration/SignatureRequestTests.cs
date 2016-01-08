// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using RealCrowd.HelloSign.Models;
using System.IO;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class SignatureRequestTests
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
        public async Task SendAndGetSignatureRequestTest()
        {
            var testEmail1 = Config.TestEmail1 ?? "test@test.com";

            var sendRequest = new SignatureRequestSendRequest
            {
                TestMode = 1,
                Signers = new List<SignerRequest>()
                {
                    new SignerRequest
                    {
                        EmailAddress = testEmail1,
                        Name = "Test Test"
                    },
                }
            };

            sendRequest.Files.AddFile("test.pdf");

            SignatureRequest signatureRequest = await client.SignatureRequest.SendAsync(sendRequest);

            // Examine returned request
            Assert.IsNotNull(signatureRequest);

            try
            {

                Assert.AreEqual(Config.Username, signatureRequest.RequesterEmailAddress);
                Assert.IsNotNull(signatureRequest.SignatureRequestId);
                Assert.AreNotEqual(string.Empty, signatureRequest.SignatureRequestId);
                Assert.AreEqual(true, signatureRequest.TestMode);
                Assert.IsNotNull(signatureRequest.DetailsUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.DetailsUrl);
                Assert.IsNotNull(signatureRequest.FilesUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.FilesUrl);
                Assert.AreEqual(false, signatureRequest.HasError);
                Assert.AreEqual(false, signatureRequest.IsComplete);
                Assert.IsNotNull(signatureRequest.SigningUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.SigningUrl);

                Assert.IsNotNull(signatureRequest.CcEmailAddresses);
                Assert.AreEqual(0, signatureRequest.CcEmailAddresses.Count);

                Assert.IsNotNull(signatureRequest.CustomFields);
                Assert.AreEqual(0, signatureRequest.CustomFields.Count);

                Assert.IsNotNull(signatureRequest.Signatures);
                Assert.AreEqual(1, signatureRequest.Signatures.Count);

                Assert.AreEqual(testEmail1, signatureRequest.Signatures[0].SignerEmailAddress);
                Assert.AreEqual("Test Test", signatureRequest.Signatures[0].SignerName);
                Assert.AreEqual(false, signatureRequest.Signatures[0].HasPin);
                Assert.IsNull(signatureRequest.Signatures[0].Order);
                Assert.AreEqual(SignatureStatusCodes.AwaitingSignature, signatureRequest.Signatures[0].StatusCode);

                Assert.IsNotNull(signatureRequest.ResponseData);
                Assert.AreEqual(0, signatureRequest.ResponseData.Count);

                Assert.IsNotNull(signatureRequest.Metadata);
                Assert.AreEqual(0, signatureRequest.Metadata.Count);

                var sigId = signatureRequest.SignatureRequestId;

                // Use Get ans examine request again
                signatureRequest = await client.SignatureRequest.GetAsync(sigId);
                Assert.IsNotNull(signatureRequest);
                Assert.AreEqual(Config.Username, signatureRequest.RequesterEmailAddress);
                Assert.AreEqual(sigId, signatureRequest.SignatureRequestId);
                Assert.AreEqual(true, signatureRequest.TestMode);
                Assert.IsNotNull(signatureRequest.DetailsUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.DetailsUrl);
                Assert.IsNotNull(signatureRequest.FilesUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.FilesUrl);
                Assert.AreEqual(false, signatureRequest.HasError);
                Assert.AreEqual(false, signatureRequest.IsComplete);
                Assert.IsNotNull(signatureRequest.SigningUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.SigningUrl);

                Assert.IsNotNull(signatureRequest.CcEmailAddresses);
                Assert.AreEqual(0, signatureRequest.CcEmailAddresses.Count);

                Assert.IsNotNull(signatureRequest.CustomFields);
                Assert.AreEqual(0, signatureRequest.CustomFields.Count);

                Assert.IsNotNull(signatureRequest.Signatures);
                Assert.AreEqual(1, signatureRequest.Signatures.Count);

                Assert.AreEqual(testEmail1, signatureRequest.Signatures[0].SignerEmailAddress);
                Assert.AreEqual("Test Test", signatureRequest.Signatures[0].SignerName);
                Assert.AreEqual(false, signatureRequest.Signatures[0].HasPin);
                Assert.IsNull(signatureRequest.Signatures[0].Order);
                Assert.AreEqual(SignatureStatusCodes.AwaitingSignature, signatureRequest.Signatures[0].StatusCode);

                Assert.IsNotNull(signatureRequest.ResponseData);
                Assert.AreEqual(0, signatureRequest.ResponseData.Count);

                Assert.IsNotNull(signatureRequest.Metadata);
                Assert.AreEqual(0, signatureRequest.Metadata.Count);
            }
            finally
            {
                var result = await client.SignatureRequest.CancelAsync(signatureRequest.SignatureRequestId);
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task SendFullSignatureRequestTest()
        {
            var testEmail1 = Config.TestEmail1 ?? "test@test.com";
            var testEmail2 = Config.TestEmail2 ?? "test2@test.com";

            var sendRequest = new SignatureRequestSendRequest
            {
                TestMode = 1,
                Title = "Test Signature Request",
                Subject = "test subject",
                Message = "test message",
                Signers = new List<SignerRequest>()
                {
                    new SignerRequest
                    {
                        EmailAddress = testEmail1,
                        Name = "Test Test",
                        Order = 0,
                        Pin = "testtesttest"
                    },
                    new SignerRequest
                    {
                        EmailAddress = testEmail2,
                        Name = "Test Test 2",
                        Order = 1,
                        Pin = "testtesttes2"
                    }
                },
                CcEmailAddresses = new List<string>()
                {
                    "testcc@test.com",
                    "testcc2@test.com"
                },
                SigningRedirectUrl = "http://test.com/redirect",
                UseTextTags = 1,
                HideTextTags = 1,
                Metadata = new Dictionary<string, string>
                {
                    { "isHelloSignApiTest", "true" },
                    { "testkey1", "testdata1" },
                    { "testkey2", "testdata2" }
                },
                //ClientId = Config.ClientId,
                FormFieldsPerDocument = new List<IList<FormFieldsRequest>>
                {
                    new List<FormFieldsRequest>
                    {
                        new FormFieldsRequest
                        {
                            ApiId = "testapiid1",
                            Height = 100,
                            Width = 101,
                            Name = "Test Field 1",
                            Required = true,
                            SignerIndex = 0,
                            ValidationType = DataValidationTypes.NumbersOnly,
                            Type = FieldTypes.Text,
                            X = 102,
                            Y = 103
                        },
                        new FormFieldsRequest
                        {
                            ApiId = "testapiid2",
                            Height = 110,
                            Width = 111,
                            Name = "Test Field 2",
                            Required = true,
                            SignerIndex = 1,
                            Type = FieldTypes.Checkbox,
                            X = 112,
                            Y = 113
                        }
                    },
                    new List<FormFieldsRequest>
                    {
                        new FormFieldsRequest
                        {
                            ApiId = "testapiid3",
                            Height = 120,
                            Width = 121,
                            Name = "Test Field 3",
                            Required = false,
                            SignerIndex = 0,
                            Type = FieldTypes.DateSigned,
                            X = 122,
                            Y = 123
                        },
                        new FormFieldsRequest
                        {
                            ApiId = "testapiid4",
                            Height = 130,
                            Width = 131,
                            Name = "Test Field 4",
                            Required = true,
                            SignerIndex = 1,
                            Type = FieldTypes.Signature,
                            X = 132,
                            Y = 133
                        }
                    }
                }
            };

            sendRequest.Files.AddFiles("test.pdf", "test2.pdf");

            SignatureRequest signatureRequest = await client.SignatureRequest.SendAsync(sendRequest);

            Assert.IsNotNull(signatureRequest);

            try
            {
                Assert.AreEqual("Test Signature Request", signatureRequest.Title);
                Assert.AreEqual("test message", signatureRequest.Message);
                Assert.AreEqual(Config.Username, signatureRequest.RequesterEmailAddress);
                Assert.IsNotNull(signatureRequest.SignatureRequestId);
                Assert.AreNotEqual(string.Empty, signatureRequest.SignatureRequestId);
                Assert.AreEqual("http://test.com/redirect", signatureRequest.SigningRedirectUrl);
                Assert.AreEqual("test subject", signatureRequest.Subject);
                Assert.AreEqual(true, signatureRequest.TestMode);
                Assert.IsNotNull(signatureRequest.DetailsUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.DetailsUrl);
                Assert.IsNotNull(signatureRequest.FilesUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.FilesUrl);
                Assert.AreEqual(false, signatureRequest.HasError);
                Assert.AreEqual(false, signatureRequest.IsComplete);
                Assert.IsNotNull(signatureRequest.SigningUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.SigningUrl);

                Assert.IsNotNull(signatureRequest.CcEmailAddresses);
                Assert.AreEqual(2, signatureRequest.CcEmailAddresses.Count);
                Assert.AreEqual("testcc@test.com", signatureRequest.CcEmailAddresses[0]);
                Assert.AreEqual("testcc2@test.com", signatureRequest.CcEmailAddresses[1]);

                Assert.IsNotNull(signatureRequest.CustomFields);
                Assert.AreEqual(0, signatureRequest.CustomFields.Count);

                Assert.IsNotNull(signatureRequest.Signatures);
                Assert.AreEqual(2, signatureRequest.Signatures.Count);

                Assert.AreEqual(testEmail1, signatureRequest.Signatures[0].SignerEmailAddress);
                Assert.AreEqual("Test Test", signatureRequest.Signatures[0].SignerName);
                Assert.AreEqual(true, signatureRequest.Signatures[0].HasPin);
                Assert.AreEqual(0, signatureRequest.Signatures[0].Order);
                Assert.AreEqual(SignatureStatusCodes.AwaitingSignature, signatureRequest.Signatures[0].StatusCode);

                Assert.AreEqual(testEmail2, signatureRequest.Signatures[1].SignerEmailAddress);
                Assert.AreEqual("Test Test 2", signatureRequest.Signatures[1].SignerName);
                Assert.AreEqual(true, signatureRequest.Signatures[1].HasPin);
                Assert.AreEqual(1, signatureRequest.Signatures[1].Order);
                Assert.AreEqual(SignatureStatusCodes.AwaitingSignature, signatureRequest.Signatures[1].StatusCode);

                Assert.IsNotNull(signatureRequest.ResponseData);
                Assert.AreEqual(0, signatureRequest.ResponseData.Count);

                Assert.IsNotNull(signatureRequest.Metadata);
                Assert.AreEqual(3, signatureRequest.Metadata.Count);
                Assert.AreEqual("true", signatureRequest.Metadata?["isHelloSignApiTest"]);
                Assert.AreEqual("testdata1", signatureRequest.Metadata?["testkey1"]);
                Assert.AreEqual("testdata2", signatureRequest.Metadata?["testkey2"]);
            }
            finally
            {
                var result = await client.SignatureRequest.CancelAsync(signatureRequest.SignatureRequestId);
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task GetFullSignatureCompletedRequestTest()
        {
            if (string.IsNullOrEmpty(Config.CompletedSignatureRequestId))
            {
                Assert.Inconclusive("Config.CompletedSignatureRequestId needs to be set before running this test");
                return;
            }

            var signatureRequest = await client.SignatureRequest.GetAsync(Config.CompletedSignatureRequestId);
            Assert.IsNotNull(signatureRequest);

            Assert.AreEqual(true, signatureRequest.IsComplete);
            Assert.IsNotNull(signatureRequest.DetailsUrl);
            Assert.AreNotEqual(string.Empty, signatureRequest.DetailsUrl);
            Assert.IsNotNull(signatureRequest.FilesUrl);
            Assert.AreNotEqual(string.Empty, signatureRequest.FilesUrl);
            Assert.IsNotNull(signatureRequest.SigningUrl);
            Assert.AreNotEqual(string.Empty, signatureRequest.SigningUrl);
            Assert.AreEqual(false, signatureRequest.HasError);

            if (string.IsNullOrEmpty(signatureRequest?.Metadata?["isHelloSignApiTest"]))
            {
                Assert.Inconclusive(
                    @"In order to fully test a completed signature request, 
it is recommended to generate the Signature Request using the SendFullSignatureTest 
method and setting the Config.CompletedSignatureRequestId to the resulting signature 
request id. You'll also want to manually complete the generated signature request before
running this test since there is not way to mimic this in code.");
                return;
            }

            var testEmail1 = Config.TestEmail1 ?? "test@test.com";
            var testEmail2 = Config.TestEmail2 ?? "test2@test.com";

            Assert.AreEqual("Test Signature Request", signatureRequest.Title);
            Assert.AreEqual("test message", signatureRequest.Message);
            Assert.AreEqual(Config.Username, signatureRequest.RequesterEmailAddress);
            Assert.AreEqual(Config.CompletedSignatureRequestId, signatureRequest.SignatureRequestId);
            Assert.AreEqual("http://test.com/redirect", signatureRequest.SigningRedirectUrl);
            Assert.AreEqual("test subject", signatureRequest.Subject);
            Assert.AreEqual(true, signatureRequest.TestMode);
            Assert.IsNotNull(signatureRequest.DetailsUrl);
            Assert.AreNotEqual(string.Empty, signatureRequest.DetailsUrl);
            Assert.IsNotNull(signatureRequest.FilesUrl);
            Assert.AreNotEqual(string.Empty, signatureRequest.FilesUrl);
            Assert.AreEqual(false, signatureRequest.HasError);
            Assert.IsNotNull(signatureRequest.SigningUrl);
            Assert.AreNotEqual(string.Empty, signatureRequest.SigningUrl);

            Assert.IsNotNull(signatureRequest.CcEmailAddresses);
            Assert.AreEqual(2, signatureRequest.CcEmailAddresses.Count);
            Assert.AreEqual("testcc@test.com", signatureRequest.CcEmailAddresses[0]);
            Assert.AreEqual("testcc2@test.com", signatureRequest.CcEmailAddresses[1]);

            Assert.IsNotNull(signatureRequest.CustomFields);
            Assert.AreEqual(0, signatureRequest.CustomFields.Count);

            Assert.IsNotNull(signatureRequest.Signatures);
            Assert.AreEqual(2, signatureRequest.Signatures.Count);

            Assert.AreEqual(testEmail1, signatureRequest.Signatures[0].SignerEmailAddress);
            Assert.AreEqual("Test Test", signatureRequest.Signatures[0].SignerName);
            Assert.AreEqual(true, signatureRequest.Signatures[0].HasPin);
            Assert.AreEqual(0, signatureRequest.Signatures[0].Order);
            Assert.AreEqual(SignatureStatusCodes.Signed, signatureRequest.Signatures[0].StatusCode);
            //Assert.IsNotNull(signatureRequest.Signatures[0].LastRemindedAt);
            Assert.IsNotNull(signatureRequest.Signatures[0].LastViewedAt);
            Assert.IsNotNull(signatureRequest.Signatures[0].SignatureId);
            Assert.IsNotNull(signatureRequest.Signatures[0].SignedAt);

            Assert.AreEqual(testEmail2, signatureRequest.Signatures[1].SignerEmailAddress);
            Assert.AreEqual("Test Test 2", signatureRequest.Signatures[1].SignerName);
            Assert.AreEqual(true, signatureRequest.Signatures[1].HasPin);
            Assert.AreEqual(1, signatureRequest.Signatures[1].Order);
            Assert.AreEqual(SignatureStatusCodes.Signed, signatureRequest.Signatures[1].StatusCode);
            //Assert.IsNotNull(signatureRequest.Signatures[1].LastRemindedAt);
            Assert.IsNotNull(signatureRequest.Signatures[1].LastViewedAt);
            Assert.IsNotNull(signatureRequest.Signatures[1].SignatureId);
            Assert.IsNotNull(signatureRequest.Signatures[1].SignedAt);

            Assert.IsNotNull(signatureRequest.ResponseData);

            // 4 Test Fields plus 2 signatures
            Assert.AreEqual(6, signatureRequest.ResponseData.Count);


            var uncheckedData = signatureRequest.ResponseData.ToList();
            var data = uncheckedData.FirstOrDefault(d => d.ApiId == "testapiid1");
            Assert.IsNotNull(data);
            Assert.AreEqual("testapiid1", data.ApiId);
            Assert.AreEqual(FieldTypes.Text, data.Type);
            Assert.AreEqual("Test Field 1", data.Name);
            Assert.AreEqual(signatureRequest.Signatures[0].SignatureId, data.SignatureId);

            uncheckedData.Remove(data);
            data = uncheckedData.FirstOrDefault(d => d.ApiId == "testapiid2");
            Assert.IsNotNull(data);
            Assert.AreEqual("testapiid2", data.ApiId);
            Assert.AreEqual(FieldTypes.Checkbox, data.Type);
            Assert.AreEqual("Test Field 2", data.Name);
            Assert.AreEqual(signatureRequest.Signatures[1].SignatureId, data.SignatureId);

            uncheckedData.Remove(data);
            data = uncheckedData.FirstOrDefault(d => d.ApiId == "testapiid3");
            Assert.IsNotNull(data);
            Assert.AreEqual("testapiid3", data.ApiId);
            Assert.AreEqual(FieldTypes.DateSigned, data.Type);
            Assert.AreEqual("Test Field 3", data.Name);
            Assert.AreEqual(signatureRequest.Signatures[0].SignatureId, data.SignatureId);

            uncheckedData.Remove(data);
            data = uncheckedData.FirstOrDefault(d => d.ApiId == "testapiid4");
            Assert.IsNotNull(data);
            Assert.AreEqual("testapiid4", data.ApiId);
            Assert.AreEqual(FieldTypes.Signature, data.Type);
            Assert.AreEqual("Test Field 4", data.Name);
            Assert.AreEqual(signatureRequest.Signatures[1].SignatureId, data.SignatureId);

            uncheckedData.Remove(data);
            data = uncheckedData.FirstOrDefault(d => d.SignatureId == signatureRequest.Signatures[0].SignatureId);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.ApiId);
            Assert.AreNotEqual(string.Empty, data.ApiId);
            Assert.AreEqual(FieldTypes.Signature, data.Type);

            uncheckedData.Remove(data);
            data = uncheckedData.FirstOrDefault(d => d.SignatureId == signatureRequest.Signatures[1].SignatureId);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.ApiId);
            Assert.AreNotEqual(string.Empty, data.ApiId);
            Assert.AreEqual(FieldTypes.Signature, data.Type);

            Assert.IsNotNull(signatureRequest.Metadata);
            Assert.AreEqual(3, signatureRequest.Metadata.Count);
            Assert.AreEqual("true", signatureRequest.Metadata?["isHelloSignApiTest"]);
            Assert.AreEqual("testdata1", signatureRequest.Metadata?["testkey1"]);
            Assert.AreEqual("testdata2", signatureRequest.Metadata?["testkey2"]);
        }

        [TestMethod]
        public async Task ListSignatureRequestsTest()
        {
            SignatureRequestList list = await client.SignatureRequest.ListAsync();
            Assert.IsTrue(list.ListInfo.NumResults > 0);
        }

        [TestMethod]
        public async Task SendWithReusableFormTest()
        {
            if (string.IsNullOrEmpty(Config.ReuseableFormId))
            {
                Assert.Inconclusive("Config.ReuseableFormId must be set to run this test. Reuseable Forms have been deprecated though, so unless for legacy purposes, ignore and use templates");
            }

            SignatureRequestSendReusableFormRequest sendRequest = new SignatureRequestSendReusableFormRequest
            {
                TestMode = 1,
                ReusableFormId = Config.ReuseableFormId,
                Title = "Test Title",
                Subject = "Test Subject",
                Message = "Test Message",
                Signers = new Dictionary<string, SignatureRequestSignerRoleRequest>()
                {
                    { "Test", new SignatureRequestSignerRoleRequest { Name = "Bob", EmailAddress = "test@test.com" } },
                    { "Test 2", new SignatureRequestSignerRoleRequest { Name = "Mary", EmailAddress = "test2@test.com" } }
                }
            };

            SignatureRequest signatureRequest = await client.SignatureRequest.SendWithReusableFormAsync(sendRequest);
            Assert.IsNotNull(signatureRequest);

            try
            {
                Assert.IsTrue(signatureRequest.Title == sendRequest.Title);
                Assert.IsTrue(signatureRequest.Subject == sendRequest.Subject);
                Assert.IsTrue(signatureRequest.Message == sendRequest.Message);
            }
            finally
            {
                var result = await client.SignatureRequest.CancelAsync(signatureRequest.SignatureRequestId);
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task SendWithSimpleTemplateTest()
        {
            if (string.IsNullOrEmpty(Config.SimpleUnorderedTemplateId))
            {
                Assert.Inconclusive("Config.SimpleTemplateId needs to be set before running this test");
                return;
            }

            var template = await client.Template.GetAsync(Config.SimpleUnorderedTemplateId);

            Assert.IsNotNull(template);

            if (template.SignerRoles.Count != 1)
            {
                Assert.Inconclusive("Template specified by Config.SimpleTemplateId must have only one SignerRole defined");
                return;
            }

            var sendRequest = new SignatureRequestFromTemplateRequest
            {
                TestMode = 1,
                TemplateIds = new List<string> { Config.SimpleUnorderedTemplateId },
                Signers = new Dictionary<string, SignatureRequestSignerRoleRequest>()
                {
                    { template.SignerRoles[0].Name, new SignatureRequestSignerRoleRequest { Name = "TestName1", EmailAddress = "test1@test.com" } },
                }
            };

            var signatureRequest = await client.SignatureRequest.SendWithTemplateAsync(sendRequest);

            Assert.IsNotNull(signatureRequest);
            Assert.IsNotNull(signatureRequest.SignatureRequestId);

            try
            {
                Assert.AreEqual(template.Title, signatureRequest.Title);
                Assert.AreEqual(template.Message, signatureRequest.Message);
                Assert.AreEqual(1, signatureRequest.Signatures.Count);
                Assert.IsNull(signatureRequest.Signatures[0].Order);
                Assert.AreEqual("test1@test.com", signatureRequest.Signatures[0].SignerEmailAddress);
                Assert.AreEqual("TestName1", signatureRequest.Signatures[0].SignerName);
                Assert.AreEqual(false, signatureRequest.Signatures[0].HasPin);
                Assert.AreEqual(SignatureStatusCodes.AwaitingSignature, signatureRequest.Signatures[0].StatusCode);
                Assert.IsNotNull(signatureRequest.CustomFields);
                Assert.AreEqual(template.Documents.Sum(d => d.CustomFields.Count), signatureRequest.CustomFields.Count);
                Assert.IsNotNull(signatureRequest.CcEmailAddresses);
                Assert.AreEqual(template.CcRoles.Count, signatureRequest.CcEmailAddresses.Count);
            }
            finally
            {

                var result = await client.SignatureRequest.CancelAsync(signatureRequest.SignatureRequestId);
                Assert.IsTrue(result);
            }
        }


        [TestMethod]
        public async Task SendWithFullAndSimpleTemplateTest()
        {
            if (string.IsNullOrEmpty(Config.SimpleUnorderedTemplateId) || string.IsNullOrEmpty(Config.FullOrderedTemplateId))
            {
                Assert.Inconclusive("Config.SimpleTemplateId and Config.FullTemplateId both needs to be set before running this test");
                return;
            }

            var fullTemplate = await client.Template.GetAsync(Config.FullOrderedTemplateId);
            var simpleTemplate = await client.Template.GetAsync(Config.SimpleUnorderedTemplateId);

            Assert.IsNotNull(fullTemplate);
            Assert.IsNotNull(simpleTemplate);

            Assert.IsTrue(fullTemplate.SignerRoles.Count > 1, "FullTemplate should have multiple signatures");

            var signers = new Dictionary<string, SignatureRequestSignerRoleRequest>();

            var signerRoles = fullTemplate.SignerRoles.Concat(simpleTemplate.SignerRoles);

            for (var i = 0; i < signerRoles.Count(); i++)
            {
                var role = signerRoles.ElementAt(i);
                signers.Add(role.Name, new SignatureRequestSignerRoleRequest() { Name = string.Format("TestName{0}", i) , EmailAddress = string.Format("test{0}@test.com", i) });
            }

            var customFields = new Dictionary<string, object>();
            var iField = 0;

            var sourceFields = fullTemplate.Documents.SelectMany(d => d.CustomFields).Concat(simpleTemplate.Documents.SelectMany(d => d.CustomFields));

            if (!sourceFields.Any(f => f.Type == FieldTypes.Checkbox) || !sourceFields.Any(f => f.Type == FieldTypes.Text))
            {
                Assert.Inconclusive("FullTemplate should have at least one text and one checkbox field for conclusive test");
            }

            foreach (var field in fullTemplate.Documents.SelectMany(d => d.CustomFields).Concat(simpleTemplate.Documents.SelectMany(d => d.CustomFields)))
            {
                iField++;

                if (field.Type == FieldTypes.Checkbox)
                    customFields.Add(field.Name,  true);
                else
                    customFields.Add(field.Name, string.Format("TestFieldValue{0}", iField));
            }

            var ccs = new Dictionary<string, string>();

            var iCC = 0;
            foreach (var roles in fullTemplate.CcRoles.Concat(simpleTemplate.CcRoles))
            {
                ccs.Add(roles.Name, string.Format("testcc{0}@test.com", iCC++));
            }

            if (ccs.Count < 2)
            {
                Assert.Inconclusive("FullTemplate should have at least two cc fields for conclusive test");
            }

            var sendRequest = new SignatureRequestFromTemplateRequest
            {
                TestMode = 1,
                TemplateIds = new List<string> { Config.FullOrderedTemplateId, Config.SimpleUnorderedTemplateId },
                Title = "Test Custom Title",
                Subject = "Test Custom Subject",
                Message = "Test Custom Message",
                Signers = signers,
                CustomFields = customFields,
                Ccs = ccs,
                //ClientId = "",
                Metadata = new Dictionary<string, string>
                {
                    { "isHelloSignApiTest", "true" },
                    { "testkey1", "testdata1" },
                    { "testkey2", "testdata2" }
                },
                SigningRedirectUrl = "http://test.com/redirect"
            };

            var signatureRequest = await client.SignatureRequest.SendWithTemplateAsync(sendRequest);

            Assert.IsNotNull(signatureRequest);
            Assert.IsNotNull(signatureRequest.SignatureRequestId);

            try
            {
                Assert.AreNotEqual(string.Empty, signatureRequest.SignatureRequestId);
                Assert.AreEqual("Test Custom Title", signatureRequest.Title);
                Assert.AreEqual("Test Custom Subject", signatureRequest.Subject);
                Assert.AreEqual("Test Custom Message", signatureRequest.Message);
                Assert.AreEqual(Config.Username, signatureRequest.RequesterEmailAddress);
                Assert.AreEqual("http://test.com/redirect", signatureRequest.SigningRedirectUrl);
                Assert.AreEqual(true, signatureRequest.TestMode);
                Assert.IsNotNull(signatureRequest.DetailsUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.DetailsUrl);
                Assert.IsNotNull(signatureRequest.FilesUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.FilesUrl);
                Assert.AreEqual(false, signatureRequest.HasError);
                Assert.AreEqual(false, signatureRequest.IsComplete);
                Assert.IsNotNull(signatureRequest.SigningUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.SigningUrl);


                Assert.IsNotNull(signatureRequest.ResponseData);
                Assert.AreEqual(0, signatureRequest.ResponseData.Count);
                Assert.IsNotNull(signatureRequest.Metadata);
                Assert.AreEqual(3, signatureRequest.Metadata.Count);
                Assert.AreEqual("true", signatureRequest.Metadata?["isHelloSignApiTest"]);
                Assert.AreEqual("testdata1", signatureRequest.Metadata?["testkey1"]);
                Assert.AreEqual("testdata2", signatureRequest.Metadata?["testkey2"]);


                Assert.AreEqual(signers.Count, signatureRequest.Signatures.Count);

                // Not Ideal, but since we can't really control the template, check based on template
                // versus literal values
                foreach (var signature in signatureRequest.Signatures)
                {
                    var signerEntry = signers.FirstOrDefault(s => s.Value.EmailAddress == signature.SignerEmailAddress);

                    Assert.IsNotNull(signerEntry);
                    Assert.AreEqual(!string.IsNullOrEmpty(signerEntry.Value.Pin), signature.HasPin);
                    Assert.IsNull(signature.LastRemindedAt);
                    Assert.IsNull(signature.LastViewedAt);

                    var role = signerRoles.FirstOrDefault(r => r.Name == signerEntry.Key);
                    Assert.AreEqual(role.Order, signature.Order);

                    Assert.IsNotNull(signatureRequest.Signatures[0].SignatureId);
                    Assert.IsNull(signatureRequest.Signatures[0].SignedAt);
                    Assert.AreEqual(signerEntry.Value.EmailAddress, signatureRequest.Signatures[0].SignerEmailAddress);
                    Assert.AreEqual(signerEntry.Value.Name, signatureRequest.Signatures[0].SignerName);
                    Assert.AreEqual(SignatureStatusCodes.AwaitingSignature, signatureRequest.Signatures[0].StatusCode);
                }

                Assert.IsNotNull(signatureRequest.CustomFields);
                Assert.AreEqual(sourceFields.Count(), signatureRequest.CustomFields.Count);

                foreach (var field in signatureRequest.CustomFields)
                {
                    var source = sourceFields.FirstOrDefault(sf => sf.Name == field.Name);
                    var sentField = customFields.FirstOrDefault(cf => cf.Key == field.Name);

                    Assert.IsNotNull(source);
                    Assert.IsNotNull(sentField);
                    Assert.AreEqual(source.Name, field.Name);
                    Assert.AreEqual(source.Type, field.Type);
                    Assert.AreEqual(sentField.Value, field.Value);
                }

                Assert.IsNotNull(signatureRequest.CcEmailAddresses);
                Assert.AreEqual(ccs.Count(), signatureRequest.CcEmailAddresses.Count);

                foreach (var cc in signatureRequest.CcEmailAddresses)
                {
                    var sourceCC = ccs.FirstOrDefault(c => c.Value == cc);

                    Assert.IsNotNull(sourceCC);
                    Assert.AreEqual(sourceCC.Value, cc);
                }
            }
            finally
            {
                var result = await client.SignatureRequest.CancelAsync(signatureRequest.SignatureRequestId);
                Assert.IsTrue(result);
            }
        }


        [TestMethod]
        public async Task SendWithFullTemplateTest()
        {
            if (string.IsNullOrEmpty(Config.FullOrderedTemplateId))
            {
                Assert.Inconclusive("Config.FullTemplateId needs to be set before running this test");
                return;
            }

            var fullTemplate = await client.Template.GetAsync(Config.FullOrderedTemplateId);

            Assert.IsNotNull(fullTemplate);

            Assert.IsTrue(fullTemplate.SignerRoles.Count > 1, "FullTemplate should have multiple signatures");

            var signers = new Dictionary<string, SignatureRequestSignerRoleRequest>();

            for (var i = 0; i < fullTemplate.SignerRoles.Count; i++)
            {
                var role = fullTemplate.SignerRoles[i];
                signers.Add(role.Name, new SignatureRequestSignerRoleRequest() { Name = string.Format("TestName{0}", i), EmailAddress = string.Format("test{0}@test.com", i) });
            }

            var customFields = new Dictionary<string, object>();
            var iField = 0;

            var sourceFields = fullTemplate.Documents.SelectMany(d => d.CustomFields);

            if (!sourceFields.Any(f => f.Type == FieldTypes.Checkbox) || !sourceFields.Any(f => f.Type == FieldTypes.Text))
            {
                Assert.Inconclusive("FullTemplate should have at least one text and one checkbox field for conclusive test");
            }

            foreach (var field in fullTemplate.Documents.SelectMany(d => d.CustomFields))
            {
                iField++;

                if (field.Type == FieldTypes.Checkbox)
                    customFields.Add(field.Name, true);
                else
                    customFields.Add(field.Name, string.Format("TestFieldValue{0}", iField));
            }

            var ccs = new Dictionary<string, string>();

            var iCC = 0;
            foreach (var roles in fullTemplate.CcRoles)
            {
                ccs.Add(roles.Name, string.Format("testcc{0}@test.com", iCC++));
            }

            if (ccs.Count < 2)
            {
                Assert.Inconclusive("FullTemplate should have at least two cc fields for conclusive test");
            }

            var sendRequest = new SignatureRequestFromTemplateRequest
            {
                TestMode = 1,
                TemplateIds = new List<string> { Config.FullOrderedTemplateId },
                Title = "Test Custom Title",
                Subject = "Test Custom Subject",
                Message = "Test Custom Message",
                Signers = signers,
                CustomFields = customFields,
                Ccs = ccs,
                //ClientId = "",
                Metadata = new Dictionary<string, string>
                {
                    { "isHelloSignApiTest", "true" },
                    { "testkey1", "testdata1" },
                    { "testkey2", "testdata2" }
                },
                SigningRedirectUrl = "http://test.com/redirect"
            };

            var signatureRequest = await client.SignatureRequest.SendWithTemplateAsync(sendRequest);

            Assert.IsNotNull(signatureRequest);
            Assert.IsNotNull(signatureRequest.SignatureRequestId);

            try
            {
                Assert.AreNotEqual(string.Empty, signatureRequest.SignatureRequestId);
                Assert.AreEqual("Test Custom Title", signatureRequest.Title);
                Assert.AreEqual("Test Custom Subject", signatureRequest.Subject);
                Assert.AreEqual("Test Custom Message", signatureRequest.Message);
                Assert.AreEqual(Config.Username, signatureRequest.RequesterEmailAddress);
                Assert.AreEqual("http://test.com/redirect", signatureRequest.SigningRedirectUrl);
                Assert.AreEqual(true, signatureRequest.TestMode);
                Assert.IsNotNull(signatureRequest.DetailsUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.DetailsUrl);
                Assert.IsNotNull(signatureRequest.FilesUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.FilesUrl);
                Assert.AreEqual(false, signatureRequest.HasError);
                Assert.AreEqual(false, signatureRequest.IsComplete);
                Assert.IsNotNull(signatureRequest.SigningUrl);
                Assert.AreNotEqual(string.Empty, signatureRequest.SigningUrl);


                Assert.IsNotNull(signatureRequest.ResponseData);
                Assert.AreEqual(0, signatureRequest.ResponseData.Count);
                Assert.IsNotNull(signatureRequest.Metadata);
                Assert.AreEqual(3, signatureRequest.Metadata.Count);
                Assert.AreEqual("true", signatureRequest.Metadata?["isHelloSignApiTest"]);
                Assert.AreEqual("testdata1", signatureRequest.Metadata?["testkey1"]);
                Assert.AreEqual("testdata2", signatureRequest.Metadata?["testkey2"]);


                Assert.AreEqual(signers.Count, signatureRequest.Signatures.Count);

                // Not Ideal, but since we can't really control the template, check based on template
                // versus literal values
                foreach (var signature in signatureRequest.Signatures)
                {
                    var signerEntry = signers.FirstOrDefault(s => s.Value.EmailAddress == signature.SignerEmailAddress);

                    Assert.IsNotNull(signerEntry);
                    Assert.AreEqual(!string.IsNullOrEmpty(signerEntry.Value.Pin), signature.HasPin);
                    Assert.IsNull(signature.LastRemindedAt);
                    Assert.IsNull(signature.LastViewedAt);

                    var role = fullTemplate.SignerRoles.FirstOrDefault(r => r.Name == signerEntry.Key);
                    Assert.AreEqual(role.Order, signature.Order);

                    Assert.IsNotNull(signature.SignatureId);
                    Assert.IsNull(signature.SignedAt);
                    Assert.AreEqual(signerEntry.Value.EmailAddress, signature.SignerEmailAddress);
                    Assert.AreEqual(signerEntry.Value.Name, signature.SignerName);
                    Assert.AreEqual(SignatureStatusCodes.AwaitingSignature, signature.StatusCode);
                }

                Assert.IsNotNull(signatureRequest.CustomFields);
                Assert.AreEqual(sourceFields.Count(), signatureRequest.CustomFields.Count);

                foreach (var field in signatureRequest.CustomFields)
                {
                    var source = sourceFields.FirstOrDefault(sf => sf.Name == field.Name);
                    var sentField = customFields.FirstOrDefault(cf => cf.Key == field.Name);

                    Assert.IsNotNull(source);
                    Assert.IsNotNull(sentField);
                    Assert.AreEqual(source.Name, field.Name);
                    Assert.AreEqual(source.Type, field.Type);
                    Assert.AreEqual(sentField.Value, field.Value);
                }

                Assert.IsNotNull(signatureRequest.CcEmailAddresses);
                Assert.AreEqual(ccs.Count(), signatureRequest.CcEmailAddresses.Count);

                foreach (var cc in signatureRequest.CcEmailAddresses)
                {
                    var sourceCC = ccs.FirstOrDefault(c => c.Value == cc);

                    Assert.IsNotNull(sourceCC);
                    Assert.AreEqual(sourceCC.Value, cc);
                }
            }
            finally
            {
                var result = await client.SignatureRequest.CancelAsync(signatureRequest.SignatureRequestId);
                Assert.IsTrue(result);
            }
        }

        public void RemindSignatureRequestTest()
        {
            
        }

        public void CancelSignatureRequestTest()
        {

        }

        [TestMethod]
        public async Task GetSignatureRequestFilesTest()
        {
            using (var response = await client.SignatureRequest.GetFilesAsync(Config.CompletedSignatureRequestId, FileTypes.Pdf))
            {

                Assert.IsNotNull(response);

                Assert.IsNotNull(response.FileName);
                Assert.AreNotEqual(string.Empty, response.FileName);
                Assert.IsNotNull(response.Stream);

                var fileName = Path.GetTempFileName();

                using (var fileStream = File.Create(fileName))
                {
                    response.Stream.Seek(0, SeekOrigin.Begin);
                    response.Stream.CopyTo(fileStream);
                }

                File.Delete(fileName);
            }
        }

        [TestMethod]
        public async Task FinalCopySignatureRequestTest()
        {
            string signatureRequestId = Config.CompletedSignatureRequestId;

            Func<FileResponse, Task> onStreamAvailable = async (fileResponse) =>
            {
                Assert.IsNotNull(fileResponse);
                Assert.IsNotNull(fileResponse.FileName);
                Assert.AreNotEqual(string.Empty, fileResponse.FileName);

                string filePath = Path.GetTempFileName();
                byte[] buffer = new byte[32768];
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    int bytesRead;
                    while ((bytesRead = fileResponse.Stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                    }
                }

                File.Delete(filePath);
            };

            SignatureRequestFinalCopyRequest request = new SignatureRequestFinalCopyRequest(onStreamAvailable)
            {
                SignatureRequestId = signatureRequestId
            };

            await client.SignatureRequest.FinalCopyAsync(request);
        }
    }
}

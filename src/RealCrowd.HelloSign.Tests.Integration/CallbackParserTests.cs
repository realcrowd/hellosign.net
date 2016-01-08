using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using RealCrowd.HelloSign.Models;

namespace RealCrowd.HelloSign.Tests.Integration
{
    [TestClass]
    public class CallbackParserTests
    {
        private const string NoAssociatedModelJsonPayloadFormat = @"{{""event"":{{""event_type"":""callback_test"",""event_time"":""{0}"",""event_hash"":""{1}"",""event_metadata"":{{""related_signature_id"":null,""reported_for_account_id"":""{2}"",""reported_for_app_id"":null,""event_message"":null}}}}}}";
        private const string RequestAssociatedModelJsonPayloadFormat = @"{{""event"":{{""event_type"":""signature_request_sent"",""event_time"":""{0}"",""event_hash"":""{1}"",""event_metadata"":{{""related_signature_id"":null,""reported_for_account_id"":""{2}"",""reported_for_app_id"":null,""event_message"":null}}}},""account_guid"":""{2}"",""client_id"":null,""signature_request"":{{""signature_request_id"":""testrequestid"",""test_mode"":true,""title"":""test"",""original_title"":""test, Signature Page"",""subject"":null,""message"":null,""metadata"":{{}},""is_complete"":false,""has_error"":false,""custom_fields"":[],""response_data"":[],""signing_url"":""https:\/\/www.hellosign.com\/sign\/test"",""signing_redirect_url"":null,""final_copy_uri"":""\/v3\/signature_request\/final_copy\/test"",""files_url"":""https:\/\/api.hellosign.com\/v3\/signature_request\/files\/test"",""details_url"":""https:\/\/www.hellosign.com\/home\/manage?guid=test"",""requester_email_address"":""{3}"",""signatures"":[{{""signature_id"":""testsigid"",""has_pin"":false,""signer_email_address"":""test@test.com"",""signer_name"":""Test Test"",""order"":null,""status_code"":""awaiting_signature"",""signed_at"":null,""last_viewed_at"":null,""last_reminded_at"":null,""error"":null}}],""cc_email_addresses"":[]}}}}";

        private HelloSignClient client;
        private JObject NoAssociatedModelJsonPayload;
        private JObject RequestAssociatedModelJsonPayload;
        private string eventHash1;
        private string eventHash2;
        private string eventTime;

        [TestInitialize]
        public void Initialize()
        {
            eventTime = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            eventHash1 = HelloSignUtilities.GenerateEventHash(Config.ApiKey, eventTime, "callback_test");
            eventHash2 = HelloSignUtilities.GenerateEventHash(Config.ApiKey, eventTime, "signature_request_sent");
            NoAssociatedModelJsonPayload = JObject.Parse(string.Format(NoAssociatedModelJsonPayloadFormat, eventTime, eventHash1, "testAccountId"));
            RequestAssociatedModelJsonPayload = JObject.Parse(string.Format(RequestAssociatedModelJsonPayloadFormat, eventTime, eventHash2, "testAccountId", Config.Username));

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
        public async Task EmptyAssociatedModelCallbackParseTest()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "http://www.test.com");
            var c = new MultipartFormDataContent();
            c.Add(new StringContent(JsonConvert.SerializeObject(NoAssociatedModelJsonPayload)), "json");
            message.Content = c;
            var callback = await client.ParseCallbackAsync(message);

            Assert.IsNotNull(callback);
            Assert.IsNotNull(callback.Event);
            Assert.AreEqual(eventHash1, callback.Event.EventHash);
            Assert.IsNotNull(callback.Event.EventMetadata);
            Assert.AreEqual("testAccountId", callback.Event.EventMetadata.ReportedForAccountId);
            Assert.IsNull(callback.Event.EventMetadata.RelatedSignatureId);
            Assert.IsNull(callback.Event.EventMetadata.ReportedForAppId);
            Assert.AreEqual(eventTime, callback.Event.EventTime);
            Assert.AreEqual("callback_test", callback.Event.EventType);
            Assert.IsNull(callback.AttachedModel);
        }

        [TestMethod]
        public async Task SignatureAssociatedModelCallbackParseTest()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "http://www.test.com");
            var c = new MultipartFormDataContent();
            c.Add(new StringContent(JsonConvert.SerializeObject(RequestAssociatedModelJsonPayload)), "json");
            message.Content = c;
            var callback = await client.ParseCallbackAsync(message);

            Assert.IsNotNull(callback);
            Assert.IsNotNull(callback.Event);
            Assert.AreEqual(eventHash2, callback.Event.EventHash);
            Assert.IsNotNull(callback.Event.EventMetadata);
            Assert.AreEqual("testAccountId", callback.Event.EventMetadata.ReportedForAccountId);
            Assert.IsNull(callback.Event.EventMetadata.RelatedSignatureId);
            Assert.IsNull(callback.Event.EventMetadata.ReportedForAppId);
            Assert.AreEqual(eventTime, callback.Event.EventTime);
            Assert.AreEqual("signature_request_sent", callback.Event.EventType);
            Assert.IsNotNull(callback.AttachedModel);
            Assert.AreEqual(callback.AttachedModel.GetType(), typeof(SignatureRequest));

            var signatureRequest = callback.AttachedModelAs<SignatureRequest>();

            Assert.IsNotNull(signatureRequest);
        }
    }
}

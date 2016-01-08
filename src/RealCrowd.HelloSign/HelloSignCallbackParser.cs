using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RealCrowd.HelloSign
{
    internal class HelloSignCallbackParser
    {
        internal string ApiKey { get; set; }

        

        public async Task<HelloSignCallback> ParseAsync(HttpRequestMessage request)
        {
            var p = new InMemoryMultipartFormDataStreamProvider();
            await request.Content.ReadAsMultipartAsync(p);

            var cb = HelloSignCallback.Parse(p.FormData["json"]);

            // only validate the hash if the ApiKey has been set
            if (string.IsNullOrEmpty(ApiKey))
            {
                var hash = HelloSignUtilities.GenerateEventHash(ApiKey, cb.Event.EventTime, cb.Event.EventType);

                if (string.IsNullOrEmpty(hash))
                {
                    throw new Exception(string.Format("Was not able to compute event hash:\r\n{0}", p.FormData["json"]));
                }

                if (hash != cb.Event.EventHash)
                {
                    throw new Exception(string.Format("Event hash does not match expected value; This event may not be genuine. Make sure this API key matches the one on the account generating callbacks:\r\n{0}", p.FormData["json"]));
                }
            }

            return cb;
        }

        private class InMemoryMultipartFormDataStreamProvider : MultipartStreamProvider
        {
            private NameValueCollection _formData = new NameValueCollection();
            private List<HttpContent> _fileContents = new List<HttpContent>();

            private List<bool> _isFormData = new List<bool>();

            public NameValueCollection FormData
            {
                get { return _formData; }
            }

            public List<HttpContent> Files
            {
                get { return _fileContents; }
            }

            public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
            {
                // For form data, Content-Disposition header is a requirement
                var contentDisposition = headers.ContentDisposition;
                if (contentDisposition != null)
                {
                    // We will post process this as form data
                    _isFormData.Add(string.IsNullOrEmpty(contentDisposition.FileName));

                    return new MemoryStream();
                }

                // If no Content-Disposition header was present.
                throw new InvalidOperationException(string.Format("Did not find required '{0}' header field in MIME multipart body part..", "Content-Disposition"));
            }

            public override async Task ExecutePostProcessingAsync()
            {
                // Find instances of non-file HttpContents and read them asynchronously
                // to get the string content and then add that as form data
                for (int index = 0; index < Contents.Count; index++)
                {
                    if (_isFormData[index])
                    {
                        HttpContent formContent = Contents[index];
                        // Extract name from Content-Disposition header. We know from earlier that the header is present.
                        ContentDispositionHeaderValue contentDisposition = formContent.Headers.ContentDisposition;
                        string formFieldName = UnquoteToken(contentDisposition.Name) ?? String.Empty;

                        // Read the contents as string data and add to form data
                        string formFieldValue = await formContent.ReadAsStringAsync();
                        FormData.Add(formFieldName, formFieldValue);
                    }
                    else
                    {
                        _fileContents.Add(Contents[index]);
                    }
                }
            }

            private static string UnquoteToken(string token)
            {
                if (String.IsNullOrWhiteSpace(token))
                {
                    return token;
                }

                if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
                {
                    return token.Substring(1, token.Length - 2);
                }

                return token;
            }
        }
    }
}

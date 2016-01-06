// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Net;
using System.Text;
using System.Threading.Tasks;
//using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace RealCrowd.HelloSign
{
    public class HelloSignService : IHelloSignService
    {
        private ISettings settings;
        private string username;
        private string password;
        private HttpClient httpClient = new HttpClient();
        

        public HelloSignService(ISettings settings, string username, string password)
        {
            this.settings = settings;
            this.username = username;
            this.password = password;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password))));
        }

        public async Task<T> MakeRequestAsync<T>(Endpoint endpoint, IHelloSignRequest request = null)
        {
            var endpointUrlAndParams = BuildEndpointUrlAndParams(endpoint, request);
            string endpointUrl = endpointUrlAndParams.Item1;
            var encodedData = endpointUrlAndParams.Item2;

            switch (endpoint.Method)
            {
                case "GET":
                    return await MakeGetRequestAsync<T>(endpointUrl, encodedData);
                case "POST":
                    return await MakePostRequestAsync<T>(endpointUrl, encodedData);
                default:
                    throw new Exception("Method not defined properly for endpoint: " + endpoint.Url);
            }
        }


        public async Task<T> MakeRequestWithFilesAsync<T>(Endpoint endpoint, IHelloSignRequestWithFiles requestWithFiles)
        {
            var endpointUrlAndParams = BuildEndpointUrlAndParams(endpoint, requestWithFiles);
            string endpointUrl = endpointUrlAndParams.Item1;
            var encodedData = endpointUrlAndParams.Item2;
            var fileRequests = endpointUrlAndParams.Item3;

            switch (endpoint.Method)
            {
                case "GET":
                    return await MakeGetRequestAsync<T>(endpointUrl, encodedData);
                case "POST":
                    return await MakePostRequestWithFilesAsync<T>(endpointUrl, encodedData, fileRequests);
                default:
                    throw new Exception("Method not defined properly for endpoint: " + endpoint.Url);
            }
        }

        public async Task MakeStreamCallbackRequestAsync(Endpoint endpoint, IHelloSignStreamCallbackRequest request)
        {
            var endpointUrlAndParams = BuildEndpointUrlAndParams(endpoint, request);
            string endpointUrl = endpointUrlAndParams.Item1;
            var encodedData = endpointUrlAndParams.Item2;

            string url = settings.HelloSignSettings.BaseUrl + endpointUrl;

            var query = ToQueryString(encodedData);
            if (!string.IsNullOrEmpty(query))
                url += "?" + query;

            using (var response = await httpClient.GetAsync(url))
            {
                await request.OnResponseStreamAvailable(new FileResponse
                {
                    Stream = await response.Content.ReadAsStreamAsync(),
                    FileName = response.Content.Headers.ContentDisposition.FileName
                });
            }
        }

        private async Task<T> MakeGetRequestAsync<T>(string endpoint, IEnumerable<KeyValuePair<string, string>> encodedDataEntries)
        {
            string url = settings.HelloSignSettings.BaseUrl + endpoint;

            var query = ToQueryString(encodedDataEntries);
            if (!string.IsNullOrEmpty(query))
                url += "?" + query;

            using (var response = await httpClient.GetAsync(url))
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }

        private async Task<T> MakePostRequestAsync<T>(string endpoint, IEnumerable<KeyValuePair<string, string>> encodedDataEntries)
        {
            string url = settings.HelloSignSettings.BaseUrl + endpoint;

            using (var response = await httpClient.PostAsync(url, new FormUrlEncodedContent(encodedDataEntries)))
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }

        private async Task<T> MakePostRequestWithFilesAsync<T>(string endpoint, IEnumerable<KeyValuePair<string, string>> encodedDataEntries, IEnumerable<KeyValuePair<string, FileRequest>> fileRequestEntries)
        {
            string url = settings.HelloSignSettings.BaseUrl + endpoint;

            var content = new MultipartFormDataContent();

            foreach (var dataEntry in encodedDataEntries)
            {
                content.Add(new StringContent(dataEntry.Value), "\"" + dataEntry.Key + "\"");
            }

            foreach (var fileRequestEntry in fileRequestEntries)
            {
                var fsContent = new StreamContent(await fileRequestEntry.Value.GetStreamAsync());
                fsContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "\"" + fileRequestEntry.Key + "\"",
                    FileName = Uri.EscapeDataString(fileRequestEntry.Value.FileName)
                };
                fsContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Add(fsContent);
            }

            using (var response = await httpClient.PostAsync(url, content))
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }

        const string PathParameterFormat = "{{{0}}}";

        private string GetStringValue(object value)
        {
            if (value is string)
                return value as string;

            return JsonConvert.SerializeObject(value);
        }

        private Tuple<string, IEnumerable<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, FileRequest>>> BuildEndpointUrlAndParams(Endpoint endpoint, IHelloSignRequestWithFiles withFilesRequest)
        {
            var EndpointAndParams = BuildEndpointUrlAndParams(endpoint, (IHelloSignRequest)withFilesRequest);

            return Tuple.Create(EndpointAndParams.Item1, EndpointAndParams.Item2, withFilesRequest.GetFileRequestEntries());
        }

        private Tuple<string, IEnumerable<KeyValuePair<string, string>>> BuildEndpointUrlAndParams(Endpoint endpoint, IHelloSignRequest request)
        {
            IDictionary<string, object> requestParams = request != null ? request.ToRequestParams() : new Dictionary<string, object>();
            if (requestParams == null)
                requestParams = new Dictionary<string, object>();

            string endpointUrl = endpoint.Url;

            List<KeyValuePair<string, string>> encodedParams = new List<KeyValuePair<string, string>>();

            foreach (var rParam in requestParams)
            {
                string stringKey = string.Format(PathParameterFormat, rParam.Key);
                if (endpointUrl.Contains(stringKey))
                    endpointUrl = endpointUrl.Replace(stringKey, GetStringValue(rParam.Value));
                else
                {
                    if (endpoint.Method == "GET")
                    {
                        encodedParams.Add(new KeyValuePair<string, string>(rParam.Key, HttpUtility.UrlEncode(GetStringValue(rParam.Value))));
                    }
                    else
                    {
                        if (rParam.Key.Contains("file"))
                        {
                            encodedParams.Add(new KeyValuePair<string, string>(rParam.Key, GetStringValue(rParam.Value)));
                        }
                        else
                        {
                            encodedParams.Add(new KeyValuePair<string, string>(rParam.Key, GetStringValue(rParam.Value)));
                        }
                    }
                }
            }

            return new Tuple<string, IEnumerable<KeyValuePair<string, string>>>(endpointUrl, encodedParams);
        }

        private static string ToQueryString(IEnumerable<KeyValuePair<string, string>> data, bool encode = false)
        {
            return string.Join("&",
                data.Select(kvp =>
                    string.Format("{0}={1}", kvp.Key, encode ? HttpUtility.UrlEncode(kvp.Value) : kvp.Value)));
        }

        public async Task<FileResponse> MakeStreamRequestAsync(Endpoint endpoint, IHelloSignRequest request)
        {
            var endpointUrlAndParams = BuildEndpointUrlAndParams(endpoint, request);
            string endpointUrl = endpointUrlAndParams.Item1;
            var encodedData = endpointUrlAndParams.Item2;

            string url = settings.HelloSignSettings.BaseUrl + endpointUrl;

            var query = ToQueryString(encodedData);
            if (!string.IsNullOrEmpty(query))
                url += "?" + query;

            
            var response = await httpClient.GetAsync(url);
            var stream = await response.Content.ReadAsStreamAsync();

            return new FileResponse()
            {
                Stream = stream,
                FileName = response.Content.Headers.ContentDisposition.FileName
            };
        }
    }
}

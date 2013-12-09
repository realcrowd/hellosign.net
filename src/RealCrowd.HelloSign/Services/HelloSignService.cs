// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RealCrowd.HelloSign
{
    public class HelloSignService : IHelloSignService
    {
        private ISettings settings;
        private string username;
        private string password;

        public HelloSignService(ISettings settings, string username, string password)
        {
            this.settings = settings;
            this.username = username;
            this.password = password;
        }

        public async Task<T> MakeRequestAsync<T>(Endpoint endpoint, IHelloSignRequest request = null)
        {
            Tuple<string, string> endpointUrlAndParams = BuildEndpointUrlAndParams(endpoint, request);
            string endpointUrl = endpointUrlAndParams.Item1;
            string dataString = endpointUrlAndParams.Item2;

            switch (endpoint.Method)
            {
                case "GET":
                    return await MakeGetRequestAsync<T>(endpointUrl, dataString);
                case "POST":
                    return await MakePostRequestAsync<T>(endpointUrl, dataString);
                default:
                    throw new Exception("Method not defined properly for endpoint: " + endpoint.Url);
            }
        }

        public async Task MakeStreamRequestAsync(Endpoint endpoint, IHelloSignStreamRequest request)
        {
            Tuple<string, string> endpointUrlAndParams = BuildEndpointUrlAndParams(endpoint, request);
            string endpointUrl = endpointUrlAndParams.Item1;
            string data = endpointUrlAndParams.Item2;

            string url = settings.HelloSignSettings.BaseUrl + endpointUrl;

            if (!string.IsNullOrEmpty(data))
                url += "?" + data;

            var webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Credentials = new NetworkCredential(username, password);
            webReq.ContentType = endpoint.ContentType;

            using (var response = await webReq.GetResponseAsync())
            {
                using (var outputStream = response.GetResponseStream())
                {
                    await request.OnStreamAvailable(outputStream);
                }
            }
        }

        private async Task<T> MakeGetRequestAsync<T>(string endpoint, string data)
        {
            string url = settings.HelloSignSettings.BaseUrl + endpoint;

            if (!string.IsNullOrEmpty(data))
                url += "?" + data;

            var webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Credentials = new NetworkCredential(username, password);
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream);
                    string responseString = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }
            }
        }

        private async Task<T> MakePostRequestAsync<T>(string endpoint, string data)
        {
            string url = settings.HelloSignSettings.BaseUrl + endpoint;
            var webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            webReq.Method = "POST";
            var bytes = Encoding.UTF8.GetBytes(data);
            webReq.ContentLength = bytes.Length;
            webReq.ContentType = "application/x-www-form-urlencoded";
            
            using (var requestStream = await webReq.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(bytes, 0, bytes.Length);
            }

            using (WebResponse response = await webReq.GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream);
                    string responseString = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }
            }
        }

        private Tuple<string, string> BuildEndpointUrlAndParams(Endpoint endpoint, IHelloSignRequest request)
        {
            IDictionary<string, object> requestParams = request != null ? request.ToRequestParams() : new Dictionary<string, object>();
            if (requestParams == null)
                requestParams = new Dictionary<string, object>();

            string endpointUrl = endpoint.Url;

            IDictionary<string, object> queryParams = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> rParam in requestParams)
            {
                string stringKey = "{" + rParam.Key + "}";
                if (endpointUrl.Contains(stringKey))
                    endpointUrl = endpointUrl.Replace(stringKey, rParam.Value.ToString());
                else
                    queryParams.Add(rParam);
            }

            return new Tuple<string, string>(endpointUrl, BuildParams(queryParams));
        }

        private string BuildParams(IDictionary<string, object> data)
        {
            if (data == null || data.Keys.Count == 0)
                return string.Empty;

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Keys.Count; i++)
            {
                string key = data.Keys.ElementAt(i);
                sBuilder.Append(key);
                sBuilder.Append("=");
                sBuilder.Append(HttpUtility.UrlEncode(data[key].ToString()));
                if (i < data.Keys.Count - 1)
                    sBuilder.Append("&");
            }
            return sBuilder.ToString();
        }
    }
}

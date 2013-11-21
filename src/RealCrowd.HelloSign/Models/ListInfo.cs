using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    public class ListInfo
    {
        [JsonProperty("num_pages")]
        public int NumPages { get; internal set; }
        [JsonProperty("num_results")]
        public int NumResults { get; internal set; }
        [JsonProperty("page")]
        public int Page { get; internal set; }
        [JsonProperty("page_size")]
        public int PageSize { get; internal set; }
    }
}

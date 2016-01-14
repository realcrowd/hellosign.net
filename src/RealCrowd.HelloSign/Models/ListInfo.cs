// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    [Serializable]
    public class ListInfo
    {
        /// <summary>
        /// Total number of pages available
        /// </summary>
        [JsonProperty("num_pages")]
        public int NumPages { get; internal set; }
        /// <summary>
        /// Total number of objects available
        /// </summary>
        [JsonProperty("num_results")]
        public int NumResults { get; internal set; }
        /// <summary>
        /// Number of the page being returned
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; internal set; }
        /// <summary>
        /// Objects returned per page
        /// </summary>
        [JsonProperty("page_size")]
        public int PageSize { get; internal set; }
    }
}

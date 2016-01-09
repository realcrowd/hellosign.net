// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign
{
    public interface IHelloSignService
    {
        Task<T> MakeRequestAsync<T>(Endpoint endpoint, IHelloSignRequest request = null);
        Task<T> MakeRequestWithFilesAsync<T>(Endpoint endpoint, IHelloSignRequestWithFiles requestWithFiles);
        Task MakeStreamCallbackRequestAsync(Endpoint endpoint, IHelloSignStreamCallbackRequest request);
        Task<FileResponse> MakeStreamRequestAsync(Endpoint endpoint, IHelloSignRequest request);
        Task<HttpResponseMessage> MakeRawRequestAsync(Endpoint endpoint, IHelloSignRequest request);
    }
}

// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign
{
    public interface IHelloSignService
    {
        Task<T> MakeRequestAsync<T>(Endpoint endpoint, IHelloSignRequest request = null);
        Task MakeStreamRequestAsync(Endpoint endpoint, IHelloSignStreamRequest request);
    }
}

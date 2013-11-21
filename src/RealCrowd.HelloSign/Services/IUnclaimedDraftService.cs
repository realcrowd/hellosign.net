// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public interface IUnclaimedDraftService
    {
        Task<UnclaimedDraft> CreateAsync(UnclaimedDraftCreateRequest request);
    }
}

// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public class UnclaimedDraftService : IUnclaimedDraftService
    {
        private ISettings settings;
        private IHelloSignService helloSignService;

        public UnclaimedDraftService(ISettings settings, IHelloSignService helloSignService)
        {
            this.settings = settings;
            this.helloSignService = helloSignService;
        }

        public async Task<UnclaimedDraft> CreateAsync(UnclaimedDraftCreateRequest request)
        {
            UnclaimedDraftWrapper wrapper = await helloSignService.MakeRequestAsync<UnclaimedDraftWrapper>(
                settings.HelloSignSettings.Endpoints.UnclaimedDraft.Create,
                request);
            return wrapper.UnclaimedDraft;
        }
    }
}

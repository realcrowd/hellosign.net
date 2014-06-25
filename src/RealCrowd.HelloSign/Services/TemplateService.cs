// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public class TemplateService : ITemplateService
    {
        private ISettings settings;
        private IHelloSignService helloSignService;

        public TemplateService(ISettings settings, IHelloSignService helloSignService)
        {
            this.settings = settings;
            this.helloSignService = helloSignService;
        }
        
        public async Task<Template> GetAsync(TemplateRequest request)
        {
            TemplateResponse templateResponse = await helloSignService.MakeRequestAsync<TemplateResponse>(
                settings.HelloSignSettings.Endpoints.Template.Get,
                request);
            return templateResponse.Template;
        }

        public async Task<TemplateListResponse> GetListAsync(TemplateListRequest request)
        {
            TemplateListResponse templateListResponse = await helloSignService.MakeRequestAsync<TemplateListResponse>(
                settings.HelloSignSettings.Endpoints.Template.List,
                request);
            return templateListResponse;
        }
    }
}

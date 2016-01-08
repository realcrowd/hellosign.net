using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public class EmbeddedService : IEmbeddedService
    {
        private ISettings settings;
        private IHelloSignService helloSignService;

        public EmbeddedService(ISettings settings, IHelloSignService helloSignService)
        {
            this.settings = settings;
            this.helloSignService = helloSignService;
        }

        public async Task<Embedded> GetSignUrlAsync(string signatureId)
        {
            return await GetSignUrlAsync(new EmbeddedGetSignUrlRequest { SignatureId = signatureId });
        }

        public async Task<Embedded> GetSignUrlAsync(EmbeddedGetSignUrlRequest request)
        {
            EmbeddedWrapper wrapper = await helloSignService.MakeRequestAsync<EmbeddedWrapper>(
                settings.HelloSignSettings.Endpoints.Embedded.GetSignUrl,
                request);
            return wrapper.Embedded;
        }

        public Task<Embedded> GetEditUrlAsync(string templateId, bool skipSignerRoles = false, bool skipSubjectAndMessage = false)
        {
            return GetEditUrlAsync(new EmbeddedGetEditUrlRequest {
                TemplateId = templateId,
                SkipSignerRoles = skipSignerRoles,
                SkipSubjectAndMessage = skipSubjectAndMessage
            });
        }

        public async Task<Embedded> GetEditUrlAsync(EmbeddedGetEditUrlRequest request)
        {
            EmbeddedWrapper wrapper = await helloSignService.MakeRequestAsync<EmbeddedWrapper>(
                settings.HelloSignSettings.Endpoints.Embedded.GetTemplateEditUrl,
                request);
            return wrapper.Embedded;
        }
    }
}

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

        public async Task<UnclaimedDraft> Create(UnclaimedDraftCreateRequest request)
        {
            UnclaimedDraftWrapper wrapper = await helloSignService.MakeRequestAsync<UnclaimedDraftWrapper>(
                settings.helloSignServiceSettings.Endpoints.UnclaimedDraft.Create,
                request);
            return wrapper.UnclaimedDraft;
        }
    }
}

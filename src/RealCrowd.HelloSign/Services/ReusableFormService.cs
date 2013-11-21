using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public class ReusableFormService : IReusableFormService
    {
        private ISettings settings;
        private IHelloSignService helloSignService;

        public ReusableFormService(ISettings settings, IHelloSignService helloSignService)
        {
            this.settings = settings;
            this.helloSignService = helloSignService;
        }

        public async Task<ReusableForm> Get(string reusableFormId)
        {
            return await Get(new ReusableFormGetRequest { ReusableFormId = reusableFormId });
        }

        public async Task<ReusableForm> Get(ReusableFormGetRequest request)
        {
            ReusableFormWrapper reusableFormWrapper = await helloSignService.MakeRequestAsync<ReusableFormWrapper>(
                settings.helloSignServiceSettings.Endpoints.ReusableForm.Get,
                request);
            return reusableFormWrapper.ReusableForm;
        }

        public async Task<ReusableFormList> List(int page)
        {
            return await List(new ReusableFormListRequest { Page = page });
        }

        public async Task<ReusableFormList> List(ReusableFormListRequest request = null)
        {
            return await helloSignService.MakeRequestAsync<ReusableFormList>(
                settings.helloSignServiceSettings.Endpoints.ReusableForm.List,
                request);
        }

        public async Task<ReusableForm> AddUser(string reusableFormId, string accountId, string emailAddress)
        {
            return await AddUser(new ReusableFormAddUserRequest { ReusableFormId = reusableFormId, AccountId = accountId, EmailAddress = emailAddress });
        }

        public async Task<ReusableForm> AddUser(ReusableFormAddUserRequest request)
        {
            ReusableFormWrapper wrapper = await helloSignService.MakeRequestAsync<ReusableFormWrapper>(
                settings.helloSignServiceSettings.Endpoints.ReusableForm.AddUser,
                request);
            return wrapper.ReusableForm;
        }

        public async Task<ReusableForm> RemoveUser(string reusableFormId, string accountId, string emailAddress)
        {
            return await RemoveUser(new ReusableFormRemoveUserRequest { ReusableFormId = reusableFormId, AccountId = accountId, EmailAddress = emailAddress });
        }

        public async Task<ReusableForm> RemoveUser(ReusableFormRemoveUserRequest request)
        {
            ReusableFormWrapper wrapper = await helloSignService.MakeRequestAsync<ReusableFormWrapper>(
                settings.helloSignServiceSettings.Endpoints.ReusableForm.RemoveUser,
                request);
            return wrapper.ReusableForm;
        }
    }
}

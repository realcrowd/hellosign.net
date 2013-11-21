// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

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

        public async Task<ReusableForm> GetAsync(string reusableFormId)
        {
            return await GetAsync(new ReusableFormGetRequest { ReusableFormId = reusableFormId });
        }

        public async Task<ReusableForm> GetAsync(ReusableFormGetRequest request)
        {
            ReusableFormWrapper reusableFormWrapper = await helloSignService.MakeRequestAsync<ReusableFormWrapper>(
                settings.HelloSignSettings.Endpoints.ReusableForm.Get,
                request);
            return reusableFormWrapper.ReusableForm;
        }

        public async Task<ReusableFormList> ListAsync(int page)
        {
            return await ListAsync(new ReusableFormListRequest { Page = page });
        }

        public async Task<ReusableFormList> ListAsync(ReusableFormListRequest request = null)
        {
            return await helloSignService.MakeRequestAsync<ReusableFormList>(
                settings.HelloSignSettings.Endpoints.ReusableForm.List,
                request);
        }

        public async Task<ReusableForm> AddUserAsync(string reusableFormId, string accountId, string emailAddress)
        {
            return await AddUserAsync(new ReusableFormAddUserRequest { ReusableFormId = reusableFormId, AccountId = accountId, EmailAddress = emailAddress });
        }

        public async Task<ReusableForm> AddUserAsync(ReusableFormAddUserRequest request)
        {
            ReusableFormWrapper wrapper = await helloSignService.MakeRequestAsync<ReusableFormWrapper>(
                settings.HelloSignSettings.Endpoints.ReusableForm.AddUser,
                request);
            return wrapper.ReusableForm;
        }

        public async Task<ReusableForm> RemoveUserAsync(string reusableFormId, string accountId, string emailAddress)
        {
            return await RemoveUserAsync(new ReusableFormRemoveUserRequest { ReusableFormId = reusableFormId, AccountId = accountId, EmailAddress = emailAddress });
        }

        public async Task<ReusableForm> RemoveUserAsync(ReusableFormRemoveUserRequest request)
        {
            ReusableFormWrapper wrapper = await helloSignService.MakeRequestAsync<ReusableFormWrapper>(
                settings.HelloSignSettings.Endpoints.ReusableForm.RemoveUser,
                request);
            return wrapper.ReusableForm;
        }
    }
}

// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public class AccountService : IAccountService
    {
        private ISettings settings;
        private IHelloSignService helloSignService;

        public AccountService(ISettings settings, IHelloSignService helloSignService)
        {
            this.settings = settings;
            this.helloSignService = helloSignService;
        }

        public async Task<Account> GetAsync()
        {
            AccountWrapper accountWrapper = await helloSignService.MakeRequestAsync<AccountWrapper>(settings.HelloSignSettings.Endpoints.Account.Get);
            return accountWrapper.Account;
        }

        public async Task<Account> UpdateAsync(string callbackUrl)
        {
            return await UpdateAsync(new AccountUpdateRequest { CallbackUrl = callbackUrl });
        }

        public async Task<Account> UpdateAsync(AccountUpdateRequest request)
        {
            AccountWrapper accountWrapper = await helloSignService.MakeRequestAsync<AccountWrapper>(
                settings.HelloSignSettings.Endpoints.Account.Update, 
                request);
            return accountWrapper.Account;
        }

        public async Task<Account> CreateAsync(string emailAddress, string password)
        {
            return await CreateAsync(new AccountCreateRequest { EmailAddress = emailAddress, Password = password });
        }

        public async Task<Account> CreateAsync(AccountCreateRequest request)
        {
            AccountWrapper accountWrapper = await helloSignService.MakeRequestAsync<AccountWrapper>(
                settings.HelloSignSettings.Endpoints.Account.Create,
                request);
            return accountWrapper.Account;
        }
    }
}

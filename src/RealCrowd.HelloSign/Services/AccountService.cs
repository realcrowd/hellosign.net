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

        public async Task<Account> Get()
        {
            AccountWrapper accountWrapper = await helloSignService.MakeRequestAsync<AccountWrapper>(settings.helloSignServiceSettings.Endpoints.Account.Get);
            return accountWrapper.Account;
        }

        public async Task<Account> Update(string callbackUrl)
        {
            return await Update(new AccountUpdateRequest { CallbackUrl = callbackUrl });
        }

        public async Task<Account> Update(AccountUpdateRequest request)
        {
            AccountWrapper accountWrapper = await helloSignService.MakeRequestAsync<AccountWrapper>(
                settings.helloSignServiceSettings.Endpoints.Account.Update, 
                request);
            return accountWrapper.Account;
        }

        public async Task<Account> Create(string emailAddress, string password)
        {
            return await Create(new AccountCreateRequest { EmailAddress = emailAddress, Password = password });
        }

        public async Task<Account> Create(AccountCreateRequest request)
        {
            AccountWrapper accountWrapper = await helloSignService.MakeRequestAsync<AccountWrapper>(
                settings.helloSignServiceSettings.Endpoints.Account.Create,
                request);
            return accountWrapper.Account;
        }
    }
}

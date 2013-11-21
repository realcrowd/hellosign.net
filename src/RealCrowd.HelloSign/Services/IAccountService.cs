using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public interface IAccountService
    {
        Task<Account> Get();
        Task<Account> Update(string callbackUrl);
        Task<Account> Update(AccountUpdateRequest request);
        Task<Account> Create(string emailAddress, string password);
        Task<Account> Create(AccountCreateRequest request);
    }
}

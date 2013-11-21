// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

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
        Task<Account> GetAsync();
        Task<Account> UpdateAsync(string callbackUrl);
        Task<Account> UpdateAsync(AccountUpdateRequest request);
        Task<Account> CreateAsync(string emailAddress, string password);
        Task<Account> CreateAsync(AccountCreateRequest request);
    }
}

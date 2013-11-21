// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public interface IReusableFormService
    {
        Task<ReusableForm> GetAsync(string reusableFormId);
        Task<ReusableForm> GetAsync(ReusableFormGetRequest request);
        Task<ReusableFormList> ListAsync(int page);
        Task<ReusableFormList> ListAsync(ReusableFormListRequest request = null);
        Task<ReusableForm> AddUserAsync(string reusableFormId, string accountId, string emailAddress);
        Task<ReusableForm> AddUserAsync(ReusableFormAddUserRequest request);
        Task<ReusableForm> RemoveUserAsync(string reusableFormId, string accountId, string emailAddress);
        Task<ReusableForm> RemoveUserAsync(ReusableFormRemoveUserRequest request);
    }
}

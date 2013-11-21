// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public interface ITeamService
    {
        Task<Team> GetAsync();
        Task<Team> CreateAsync(string name);
        Task<Team> CreateAsync(TeamCreateRequest request);
        Task<Team> UpdateAsync(string name);
        Task<Team> UpdateAsync(TeamUpdateRequest request);
        Task<bool> DestroyAsync();
        Task<Team> AddMemberAsync(string accountId, string emailAddress);
        Task<Team> AddMemberAsync(TeamAddMemberRequest request);
        Task<Team> RemoveMemberAsync(string accountId, string emailAddress);
        Task<Team> RemoveMemberAsync(TeamRemoveMemberRequest request);
    }
}

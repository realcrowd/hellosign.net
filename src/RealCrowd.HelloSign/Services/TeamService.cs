// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public class TeamService : ITeamService
    {
        private ISettings settings;
        private IHelloSignService helloSignService;

        public TeamService(ISettings settings, IHelloSignService helloSignService)
        {
            this.settings = settings;
            this.helloSignService = helloSignService;
        }

        public async Task<Team> GetAsync()
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.HelloSignSettings.Endpoints.Team.Get);
            return wrapper.Team;
        }

        public async Task<Team> CreateAsync(string name)
        {
            return await CreateAsync(new TeamCreateRequest { Name = name });
        }

        public async Task<Team> CreateAsync(TeamCreateRequest request)
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.HelloSignSettings.Endpoints.Team.Create,
                request);
            return wrapper.Team;
        }

        public async Task<Team> UpdateAsync(string name)
        {
            return await UpdateAsync(new TeamUpdateRequest { Name = name });
        }

        public async Task<Team> UpdateAsync(TeamUpdateRequest request)
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.HelloSignSettings.Endpoints.Team.Update,
                request);
            return wrapper.Team;
        }

        public async Task<bool> DestroyAsync()
        {
            dynamic response = await helloSignService.MakeRequestAsync<dynamic>(
                settings.HelloSignSettings.Endpoints.Team.Delete);
            return response != null;
        }

        public async Task<Team> AddMemberAsync(string accountId, string emailAddress)
        {
            return await AddMemberAsync(new TeamAddMemberRequest { AccountId = accountId, EmailAddress = emailAddress });
        }

        public async Task<Team> AddMemberAsync(TeamAddMemberRequest request)
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.HelloSignSettings.Endpoints.Team.AddMember,
                request);
            return wrapper.Team;
        }

        public async Task<Team> RemoveMemberAsync(string accountId, string emailAddress)
        {
            return await RemoveMemberAsync(new TeamRemoveMemberRequest { AccountId = accountId, EmailAddress = emailAddress });
        }

        public async Task<Team> RemoveMemberAsync(TeamRemoveMemberRequest request)
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.HelloSignSettings.Endpoints.Team.RemoveMember,
                request);
            return wrapper.Team;
        }
    }
}

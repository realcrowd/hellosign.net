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

        public async Task<Team> Get()
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.helloSignServiceSettings.Endpoints.Team.Get);
            return wrapper.Team;
        }

        public async Task<Team> Create(string name)
        {
            return await Create(new TeamCreateRequest { Name = name });
        }

        public async Task<Team> Create(TeamCreateRequest request)
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.helloSignServiceSettings.Endpoints.Team.Create,
                request);
            return wrapper.Team;
        }

        public async Task<Team> Update(string name)
        {
            return await Update(new TeamUpdateRequest { Name = name });
        }

        public async Task<Team> Update(TeamUpdateRequest request)
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.helloSignServiceSettings.Endpoints.Team.Update,
                request);
            return wrapper.Team;
        }

        public async Task<bool> Destroy()
        {
            dynamic response = await helloSignService.MakeRequestAsync<dynamic>(
                settings.helloSignServiceSettings.Endpoints.Team.Destory);
            return response != null;
        }

        public async Task<Team> AddMember(string accountId, string emailAddress)
        {
            return await AddMember(new TeamAddMemberRequest { AccountId = accountId, EmailAddress = emailAddress });
        }

        public async Task<Team> AddMember(TeamAddMemberRequest request)
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.helloSignServiceSettings.Endpoints.Team.AddMember,
                request);
            return wrapper.Team;
        }

        public async Task<Team> RemoveMember(string accountId, string emailAddress)
        {
            return await RemoveMember(new TeamRemoveMemberRequest { AccountId = accountId, EmailAddress = emailAddress });
        }

        public async Task<Team> RemoveMember(TeamRemoveMemberRequest request)
        {
            TeamWrapper wrapper = await helloSignService.MakeRequestAsync<TeamWrapper>(
                settings.helloSignServiceSettings.Endpoints.Team.RemoveMember,
                request);
            return wrapper.Team;
        }
    }
}

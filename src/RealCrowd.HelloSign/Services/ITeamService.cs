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
        Task<Team> Get();
        Task<Team> Create(string name);
        Task<Team> Create(TeamCreateRequest request);
        Task<Team> Update(string name);
        Task<Team> Update(TeamUpdateRequest request);
        Task<bool> Destroy();
        Task<Team> AddMember(string accountId, string emailAddress);
        Task<Team> AddMember(TeamAddMemberRequest request);
        Task<Team> RemoveMember(string accountId, string emailAddress);
        Task<Team> RemoveMember(TeamRemoveMemberRequest request);
    }
}

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
        Task<ReusableForm> Get(string reusableFormId);
        Task<ReusableForm> Get(ReusableFormGetRequest request);
        Task<ReusableFormList> List(int page);
        Task<ReusableFormList> List(ReusableFormListRequest request = null);
        Task<ReusableForm> AddUser(string reusableFormId, string accountId, string emailAddress);
        Task<ReusableForm> AddUser(ReusableFormAddUserRequest request);
        Task<ReusableForm> RemoveUser(string reusableFormId, string accountId, string emailAddress);
        Task<ReusableForm> RemoveUser(ReusableFormRemoveUserRequest request);
    }
}

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public interface ISignatureRequestService
    {
        Task<SignatureRequest> Get(string signatureRequestId);
        Task<SignatureRequest> Get(SignatureRequestGetRequest request);
        Task<SignatureRequestList> List(int page);
        Task<SignatureRequestList> List(SignatureRequestListRequest request = null);
        Task<SignatureRequest> Send(SignatureRequestSendRequest sendRequest);
        Task<SignatureRequest> SendWithReusableForm(SignatureRequestSendReusableFormRequest sendRequest);
        Task<SignatureRequest> Remind(string signatureRequestId, string emailAddress);
        Task<SignatureRequest> Remind(SignatureRequestRemindRequest request);
        Task<bool> Cancel(string signatureRequestId);
        Task<bool> Cancel(SignatureRequestCancelRequest request);
        Task<byte[]> FinalCopy(string signatureRequestId);
        Task<byte[]> FinalCopy(SignatureRequestFinalCopyRequest request);
    }
}

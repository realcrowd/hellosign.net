// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public interface ISignatureRequestService
    {
        Task<SignatureRequest> GetAsync(string signatureRequestId);
        Task<SignatureRequest> GetAsync(SignatureRequestGetRequest request);
        Task<SignatureRequestList> ListAsync(int page);
        Task<SignatureRequestList> ListAsync(SignatureRequestListRequest request = null);
        Task<SignatureRequest> SendAsync(SignatureRequestSendRequest sendRequest);
        Task<SignatureRequest> SendWithReusableFormAsync(SignatureRequestSendReusableFormRequest sendRequest);
        Task<SignatureRequest> RemindAsync(string signatureRequestId, string emailAddress);
        Task<SignatureRequest> RemindAsync(SignatureRequestRemindRequest request);
        Task<bool> CancelAsync(string signatureRequestId);
        Task<bool> CancelAsync(SignatureRequestCancelRequest request);
        Task FinalCopyAsync(string signatureRequestId, Action<Stream> onStreamAvailable);
        Task FinalCopyAsync(SignatureRequestFinalCopyRequest request);
        Task<List<SignatureRequest>> CreateEmbeddedWithReusableFormAsync(SignatureRequestSendReusableFormRequest request);
        Task<List<SignatureRequest>> CreateEmbeddedAsync(SignatureRequestSendRequest request);
    }
}

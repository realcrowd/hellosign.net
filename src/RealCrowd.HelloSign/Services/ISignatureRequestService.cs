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
        /// <summary>
        /// Gets a SignatureRequest that includes the current status for each signer.
        /// </summary>
        /// <param name="signatureRequestId">The id of the SignatureRequest to retrieve.</param>
        /// <returns>
        /// Returns the status of the SignatureRequest specified by the signature_request_id parameter.
        /// </returns>
        Task<SignatureRequest> GetAsync(string signatureRequestId);
        /// <summary>
        /// Gets a SignatureRequest that includes the current status for each signer.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the status of the SignatureRequest specified by the signature_request_id parameter.</returns>
        Task<SignatureRequest> GetAsync(SignatureRequestGetRequest request);

        /// <summary>
        /// Lists the SignatureRequests (both inbound and outbound) that you have access to.
        /// </summary>
        /// <param name="page">
        /// Which page number of the SignatureRequest List to return. Defaults to 1.
        /// </param>
        /// <param name="pageSize">
        /// Number of objects to be returned per page. Must be between 1 and 100. Default is 20.
        /// </param>
        /// <param name="accountId">
        /// Which account to return SignatureRequests for. Must be a team member. Use "all" to indicate all team members. Defaults to your account.
        /// </param>
        /// <param name="query">
        /// String that includes search terms and/or fields to be used to filter the SignatureRequest objects.
        /// </param>
        /// <returns>
        /// Returns a list of SignatureRequests that you can access. This includes SignatureRequests you have sent as well as received, but not ones that you have been CCed on.
        /// </returns>
        Task<SignatureRequestList> ListAsync(int? page, int? pageSize, string accountId, string query = null);

        /// <summary>
        /// Lists the SignatureRequests (both inbound and outbound) that you have access to.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// Returns a list of SignatureRequests that you can access. This includes SignatureRequests you have sent as well as received, but not ones that you have been CCed on.
        /// </returns>
        Task<SignatureRequestList> ListAsync(SignatureRequestListRequest request = null);

        /// <summary>
        /// Gets a SignatureRequest that includes the current status for each signer.
        /// </summary>
        /// <param name="sendRequest"></param>
        /// <returns>Returns the status of the SignatureRequest specified by the signature_request_id parameter.</returns>
        Task<SignatureRequest> SendAsync(SignatureRequestSendRequest sendRequest);
        
        /// <summary>
        /// Creates and sends a new SignatureRequest based off of a Template.
        /// </summary>
        /// <param name="sendRequest"></param>
        /// <returns></returns>
        Task<SignatureRequest> SendWithTemplateAsync(SignatureRequestFromTemplateRequest sendRequest);
        Task<SignatureRequest> RemindAsync(string signatureRequestId, string emailAddress);
        Task<SignatureRequest> RemindAsync(SignatureRequestRemindRequest request);
        Task<bool> CancelAsync(string signatureRequestId);
        Task<bool> CancelAsync(SignatureRequestCancelRequest request);
        Task GetFilesAsync(string signatureRequestId, string fileType, Func<FileResponse, Task> onStreamAvailable);
        Task GetFilesAsync(SignatureRequestGetFilesCallbackRequest request);
        Task<FileResponse> GetFilesAsync(string signatureRequestId, string fileType);
        Task<FileResponse> GetFilesAsync(SignatureRequestGetFilesRequest request);

        Task<List<SignatureRequest>> SendEmbeddedAsync(SignatureRequestSendRequest request);
        Task<SignatureRequest> SendEmbeddedWithTemplateAsync(EmbeddedSignatureFromTemplateRequest request);

        [Obsolete]
        Task<SignatureRequest> SendWithReusableFormAsync(SignatureRequestSendReusableFormRequest sendRequest);
        [Obsolete]
        Task FinalCopyAsync(string signatureRequestId, Func<FileResponse, Task> onStreamAvailable);
        [Obsolete]
        Task FinalCopyAsync(SignatureRequestFinalCopyRequest request);
        [Obsolete]
        Task<List<SignatureRequest>> CreateEmbeddedWithReusableFormAsync(SignatureRequestSendReusableFormRequest request);
    }
}

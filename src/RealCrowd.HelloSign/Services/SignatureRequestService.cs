// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace RealCrowd.HelloSign.Clients
{
    public class SignatureRequestService : ISignatureRequestService
    {
        private ISettings settings;
        private IHelloSignService helloSignService;

        public SignatureRequestService(ISettings settings, IHelloSignService helloSignService)
        {
            this.settings = settings;
            this.helloSignService = helloSignService;
        }

        public async Task<SignatureRequest> GetAsync(string signatureRequestId)
        {
            return await GetAsync(new SignatureRequestGetRequest { SignatureRequestId = signatureRequestId });
        }

        public async Task<SignatureRequest> GetAsync(SignatureRequestGetRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.Get,
                request);
            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<SignatureRequestList> ListAsync(int page)
        {
            return await ListAsync(new SignatureRequestListRequest { Page = page });
        }

        public Task<SignatureRequestList> ListAsync(int? page, int? pageSize, string accountId, string query = null)
        {
            return ListAsync(new SignatureRequestListRequest {
                Page = page,
                PageSize = pageSize,
                AccountId = accountId,
                Query = query
            });
        }

        public async Task<SignatureRequestList> ListAsync(SignatureRequestListRequest request = null)
        {
            return await helloSignService.MakeRequestAsync<SignatureRequestList>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.List,
                request != null ? request : new SignatureRequestListRequest());
        }

        public async Task<SignatureRequest> SendAsync(SignatureRequestSendRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestWithFilesAsync<SignatureRequestWrapper>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.Send,
                request);

            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<SignatureRequest> SendWithTemplateAsync(SignatureRequestFromTemplateRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.SendWithTemplate,
                request);

            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<SignatureRequest> RemindAsync(string signatureRequestId, string emailAddress)
        {
            return await RemindAsync(new SignatureRequestRemindRequest { SignatureRequestId = signatureRequestId, EmailAddress = emailAddress });
        }

        public async Task<SignatureRequest> RemindAsync(SignatureRequestRemindRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.Remind,
                request);
            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<bool> CancelAsync(string signatureRequestId)
        {
            return await CancelAsync(new SignatureRequestCancelRequest { SignatureRequestId = signatureRequestId });
        }

        public async Task<bool> CancelAsync(SignatureRequestCancelRequest request)
        {
            dynamic response = await helloSignService.MakeRequestAsync<dynamic>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.Cancel,
                request);
            return true;
        }

        public async Task GetFilesAsync(string signatureRequestId, string fileType, Func<FileResponse, Task> onStreamAvailable)
        {
            await GetFilesAsync(new SignatureRequestGetFilesCallbackRequest(onStreamAvailable) { SignatureRequestId = signatureRequestId, FileType = fileType });
        }

        public async Task GetFilesAsync(SignatureRequestGetFilesCallbackRequest request)
        {
            await helloSignService.MakeStreamCallbackRequestAsync(settings.HelloSignSettings.Endpoints.SignatureRequest.GetFiles,
                request);
        }

        public Task<FileResponse> GetFilesAsync(string signatureRequestId, string fileType)
        {
            return GetFilesAsync(new SignatureRequestGetFilesRequest
            {
                SignatureRequestId = signatureRequestId,
                FileType = fileType
            });
        }

        public Task<FileResponse> GetFilesAsync(SignatureRequestGetFilesRequest request)
        {
            return helloSignService.MakeStreamRequestAsync(settings.HelloSignSettings.Endpoints.SignatureRequest.GetFiles,
                request);
        }

        public async Task<List<SignatureRequest>> SendEmbeddedAsync(SignatureRequestSendRequest request)
        {
            return await helloSignService.MakeRequestAsync<List<SignatureRequest>>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.SendEmbedded,
                request);
        }
        public async Task<SignatureRequest> SendEmbeddedWithTemplateAsync(EmbeddedSignatureFromTemplateRequest request)
        {
            var signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.SendEmbeddedWithTemplate,
                 request);
            return signatureRequestWrapper.SignatureRequest;
        }

        [Obsolete("Use GetFiles")]
        public async Task FinalCopyAsync(string signatureRequestId, Func<FileResponse, Task> onStreamAvailable)
        {
            await FinalCopyAsync(new SignatureRequestFinalCopyRequest(onStreamAvailable) { SignatureRequestId = signatureRequestId });
        }

        [Obsolete("Use GetFiles")]
        public async Task FinalCopyAsync(SignatureRequestFinalCopyRequest request)
        {
            await helloSignService.MakeStreamCallbackRequestAsync(settings.HelloSignSettings.Endpoints.SignatureRequest.GetFinalCopy,
                request);
        }

        [Obsolete("Use SendEmbeddedWithTemplate")]
        public async Task<List<SignatureRequest>> CreateEmbeddedWithReusableFormAsync(SignatureRequestSendReusableFormRequest request)
        {
            return await helloSignService.MakeRequestAsync<List<SignatureRequest>>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.CreateEmbeddedWithReusableForm,
                request);
        }

        [Obsolete("Use SendWithTemplate")]
        public async Task<SignatureRequest> SendWithReusableFormAsync(SignatureRequestSendReusableFormRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.SendForm,
                request);

            return signatureRequestWrapper.SignatureRequest;
        }
    }
}

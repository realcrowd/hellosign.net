using RealCrowd.HelloSign.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<SignatureRequestList> ListAsync(SignatureRequestListRequest request = null)
        {
            return await helloSignService.MakeRequestAsync<SignatureRequestList>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.List,
                request != null ? request : new SignatureRequestListRequest());
        }

        public async Task<SignatureRequest> SendAsync(SignatureRequestSendRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.Send,
                request);

            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<SignatureRequest> SendWithReusableFormAsync(SignatureRequestSendReusableFormRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.HelloSignSettings.Endpoints.SignatureRequest.SendForm,
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
                settings.HelloSignSettings.Endpoints.SignatureRequest.Remind,
                request);
            return response != null;
        }

        public async Task<byte[]> FinalCopyAsync(string signatureRequestId)
        {
            return await FinalCopyAsync(new SignatureRequestFinalCopyRequest { SignatureRequestId = signatureRequestId });
        }

        public async Task<byte[]> FinalCopyAsync(SignatureRequestFinalCopyRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

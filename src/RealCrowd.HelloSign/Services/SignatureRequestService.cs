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

        public async Task<SignatureRequest> Get(string signatureRequestId)
        {
            return await Get(new SignatureRequestGetRequest { SignatureRequestId = signatureRequestId });
        }

        public async Task<SignatureRequest> Get(SignatureRequestGetRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.helloSignServiceSettings.Endpoints.SignatureRequest.Get,
                request);
            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<SignatureRequestList> List(int page)
        {
            return await List(new SignatureRequestListRequest { Page = page });
        }

        public async Task<SignatureRequestList> List(SignatureRequestListRequest request = null)
        {
            return await helloSignService.MakeRequestAsync<SignatureRequestList>(
                settings.helloSignServiceSettings.Endpoints.SignatureRequest.List,
                request != null ? request : new SignatureRequestListRequest());
        }

        public async Task<SignatureRequest> Send(SignatureRequestSendRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.helloSignServiceSettings.Endpoints.SignatureRequest.Send,
                request);

            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<SignatureRequest> SendWithReusableForm(SignatureRequestSendReusableFormRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.helloSignServiceSettings.Endpoints.SignatureRequest.SendForm,
                request);

            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<SignatureRequest> Remind(string signatureRequestId, string emailAddress)
        {
            return await Remind(new SignatureRequestRemindRequest { SignatureRequestId = signatureRequestId, EmailAddress = emailAddress });
        }

        public async Task<SignatureRequest> Remind(SignatureRequestRemindRequest request)
        {
            SignatureRequestWrapper signatureRequestWrapper = await helloSignService.MakeRequestAsync<SignatureRequestWrapper>(
                settings.helloSignServiceSettings.Endpoints.SignatureRequest.Remind,
                request);
            return signatureRequestWrapper.SignatureRequest;
        }

        public async Task<bool> Cancel(string signatureRequestId)
        {
            return await Cancel(new SignatureRequestCancelRequest { SignatureRequestId = signatureRequestId });
        }

        public async Task<bool> Cancel(SignatureRequestCancelRequest request)
        {
            dynamic response = await helloSignService.MakeRequestAsync<dynamic>(
                settings.helloSignServiceSettings.Endpoints.SignatureRequest.Remind,
                request);
            return response != null;
        }

        public async Task<byte[]> FinalCopy(string signatureRequestId)
        {
            return await FinalCopy(new SignatureRequestFinalCopyRequest { SignatureRequestId = signatureRequestId });
        }

        public async Task<byte[]> FinalCopy(SignatureRequestFinalCopyRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.
using RealCrowd.HelloSign.Clients;
using Ninject;

namespace RealCrowd.HelloSign
{
    public class HelloSignClient
    {
        private IKernel kernel;

        public HelloSignClient(string apiKey)
            : this(apiKey, string.Empty)
        { }

        public HelloSignClient(string username, string password)
        {
            LoadDependencies(username, password);
        }

        private void LoadDependencies(string username, string password)
        {
            kernel = new StandardKernel();

            kernel
                .Bind<ISettings>()
                .To<Settings>()
                .InSingletonScope();

            kernel
                .Bind<IHelloSignService>()
                .To<HelloSignService>()
                .InSingletonScope()
                .WithConstructorArgument("username", username)
                .WithConstructorArgument("password", password);
            
            kernel
                .Bind<IAccountService>()
                .To<AccountService>()
                .InSingletonScope();

            kernel
                .Bind<ISignatureRequestService>()
                .To<SignatureRequestService>()
                .InSingletonScope();

            kernel
                .Bind<IReusableFormService>()
                .To<ReusableFormService>()
                .InSingletonScope();

            kernel
                .Bind<ITeamService>()
                .To<TeamService>()
                .InSingletonScope();

            kernel
                .Bind<IUnclaimedDraftService>()
                .To<UnclaimedDraftService>()
                .InSingletonScope();

            kernel
                .Bind<IEmbeddedService>()
                .To<EmbeddedService>()
                .InSingletonScope();

            kernel
               .Bind<ITemplateService>()
               .To<TemplateService>()
               .InSingletonScope();
        }

        private IAccountService account;
        public IAccountService Account
        {
            get 
            {
                if (account == null)
                    account = kernel.Get<IAccountService>();
                return account; 
            }
        }

        private ISignatureRequestService signatureRequest;
        public ISignatureRequestService SignatureRequest
        {
            get 
            {
                if (signatureRequest == null)
                    signatureRequest = kernel.Get<ISignatureRequestService>();
                return signatureRequest; 
            }
        }

        private IReusableFormService reusableFormClient;
        public IReusableFormService ReusableForm
        {
            get 
            {
                if (reusableFormClient == null)
                    reusableFormClient = kernel.Get<IReusableFormService>();
                return reusableFormClient; 
            }
        }

        private ITeamService team;
        public ITeamService Team
        {
            get 
            {
                if (team == null)
                    team = kernel.Get<ITeamService>();
                return team;
            }
        }
        private ITemplateService template;
        public ITemplateService Template
        {
            get
            {
                if (template == null)
                    template = kernel.Get<ITemplateService>();
                return template;
            }
        }

        private IUnclaimedDraftService unclaimedDraft;
        public IUnclaimedDraftService UnclaimedDraft
        {
            get 
            { 
                if (unclaimedDraft == null)
                    unclaimedDraft = kernel.Get<IUnclaimedDraftService>();
                return unclaimedDraft;
            }
        }
        private IEmbeddedService embedded;
        public IEmbeddedService Embedded
        {
            get
            {
                if (embedded == null)
                    embedded = kernel.Get<IEmbeddedService>();
                return embedded;
            }
        }
    }
}

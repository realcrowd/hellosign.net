// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using RealCrowd.HelloSign.Models;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public interface ITemplateService
    {
        Task<Template> GetAsync(TemplateRequest request);
        Task<TemplateListResponse> GetListAsync(TemplateListRequest request);
    }
}

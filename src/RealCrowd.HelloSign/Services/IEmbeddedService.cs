using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Clients
{
    public interface IEmbeddedService
    {
        Task<Embedded> GetSignUrlAsync(string signatureId);
        Task<Embedded> GetSignUrlAsync(EmbeddedGetSignUrlRequest request);
    }
}

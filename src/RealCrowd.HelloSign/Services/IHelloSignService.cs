using RealCrowd.HelloSign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign
{
    public interface IHelloSignService
    {
        Task<T> MakeRequestAsync<T>(Endpoint endpoint, IHelloSignRequest request = null);
    }
}

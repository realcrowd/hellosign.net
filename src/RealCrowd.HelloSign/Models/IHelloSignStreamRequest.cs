using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    public interface IHelloSignStreamRequest : IHelloSignRequest
    {
        Func<Stream, Task> OnStreamAvailable { get; set; }
    }
}

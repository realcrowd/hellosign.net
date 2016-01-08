using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    public interface IHelloSignRequestWithFiles : IHelloSignRequest
    {
        IEnumerable<KeyValuePair<string, FileRequest>> GetFileRequestEntries();
    }
}

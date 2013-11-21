using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Tests.Integration
{
    public class Config
    {
        public static string Username { get { return Environment.GetEnvironmentVariable("HELLOSIGN_USERNAME", EnvironmentVariableTarget.User); } }
        public static string Password { get { return Environment.GetEnvironmentVariable("HELLOSIGN_PASSWORD", EnvironmentVariableTarget.User); } }
    }
}

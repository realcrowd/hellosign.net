// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

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
        public static string ClientId { get { return Environment.GetEnvironmentVariable("HELLOSIGN_CLIENT_ID", EnvironmentVariableTarget.User); } }
        public static string TemplateId1 { get { return Environment.GetEnvironmentVariable("HELLOSIGN_TEMPLATE_ID_1", EnvironmentVariableTarget.User); } }
    }
}

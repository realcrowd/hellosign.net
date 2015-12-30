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
        public static string ApiKey { get { return Environment.GetEnvironmentVariable("HELLOSIGN_API_KEY", EnvironmentVariableTarget.User); } }
        public static string Username { get { return Environment.GetEnvironmentVariable("HELLOSIGN_USERNAME", EnvironmentVariableTarget.User); } }
        public static string Password { get { return Environment.GetEnvironmentVariable("HELLOSIGN_PASSWORD", EnvironmentVariableTarget.User); } }
        public static string ClientId { get { return Environment.GetEnvironmentVariable("HELLOSIGN_CLIENT_ID", EnvironmentVariableTarget.User); } }
        public static string TemplateId1 { get { return Environment.GetEnvironmentVariable("HELLOSIGN_TEMPLATE_ID_1", EnvironmentVariableTarget.User); } }
        public static string ReuseableFormId { get { return Environment.GetEnvironmentVariable("HELLOSIGN_REUSEABLE_FORM_ID", EnvironmentVariableTarget.User); } }
        //public static string TemplateId2 { get { return Environment.GetEnvironmentVariable("HELLOSIGN_TEMPLATE_ID_2", EnvironmentVariableTarget.User); } }
        /// <summary>
        /// Id for a Template that has a single signer role
        /// </summary>
        public static string SimpleUnorderedTemplateId { get { return Environment.GetEnvironmentVariable("HELLOSIGN_SIMPLE_TEMPLATE_ID", EnvironmentVariableTarget.User); } }
        public static string FullOrderedTemplateId { get { return Environment.GetEnvironmentVariable("HELLOSIGN_FULL_TEMPLATE_ID", EnvironmentVariableTarget.User); } }
        public static string FullUnorderedTemplateId { get { return Environment.GetEnvironmentVariable("HELLOSIGN_FULL_TEMPLATE_ID", EnvironmentVariableTarget.User); } }
        /// <summary>
        /// Signature Request Id of a request that has been completed. This cannot be duplicated in 
        /// code so completing a request must be done manually.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static string CompletedSignatureRequestId { get { return Environment.GetEnvironmentVariable("HELLOSIGN_COMPLETED_SIGNATURE_REQUEST_ID", EnvironmentVariableTarget.User); } }
        public static string TestEmail1 { get { return Environment.GetEnvironmentVariable("HELLOSIGN_TEST_EMAIL_1", EnvironmentVariableTarget.User); } }
        public static string TestEmail2 { get { return Environment.GetEnvironmentVariable("HELLOSIGN_TEST_EMAIL_2", EnvironmentVariableTarget.User); } }
    }
}

using System;
using System.Text;

namespace RealCrowd.HelloSign
{
    public static class HelloSignUtilities
    {
        public static string GenerateEventHash(string apiKey, string eventTime, string eventType)
        {
            var keyBytes = Encoding.ASCII.GetBytes(apiKey);
            using (var hmac = new System.Security.Cryptography.HMACSHA256(keyBytes))
            {
                var inputBytes = Encoding.ASCII.GetBytes(eventTime + eventType);
                var outputBytes = hmac.ComputeHash(inputBytes);
                return BitConverter.ToString(outputBytes).Replace("-", "").ToLower();
            }
        }
    }
}

using Newtonsoft.Json;

namespace RealCrowd.HelloSign.Models
{
    public class CustomField : CustomFieldTemplate
    {
        [JsonProperty("value")]
        public string Value { get; internal set; }
    }
}

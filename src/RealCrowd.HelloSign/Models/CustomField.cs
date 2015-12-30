using Newtonsoft.Json;

namespace RealCrowd.HelloSign.Models
{
    public class CustomField : CustomFieldTemplate
    {
        [JsonProperty("value")]
        public object Value { get; internal set; }
    }
}

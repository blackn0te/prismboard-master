using Newtonsoft.Json;

namespace MvcForum.Core.VirusTotal.Objects
{
    public class WebutationInfo
    {
        [JsonProperty("Adult content")]
        public string AdultContent { get; set; }

        [JsonProperty("Safety score")]
        public int SafetyScore { get; set; }

        public string Verdict { get; set; }
    }
}
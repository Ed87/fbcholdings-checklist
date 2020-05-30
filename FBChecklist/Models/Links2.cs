using Newtonsoft.Json;

namespace FBChecklist.Models
{
    public class Links2
    {
        [JsonProperty("rel")]
        public string rel { get; set; }

        [JsonProperty("uri")]
        public string uri { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FBChecklist.Models
{
    public class Root
    {
        [JsonProperty("items")]
        public List<WebLogic> items { get; set; }

        [JsonProperty("links")]
        public Links2[] links { get; set; }
    }
}
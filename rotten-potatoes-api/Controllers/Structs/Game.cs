using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace rotten_potatoes_api.Controllers
{
    public struct Game
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("cover")]
        public Cover Cover { get; set; }
    }
}

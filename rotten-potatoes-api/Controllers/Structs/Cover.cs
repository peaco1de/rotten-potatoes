using System.Text.Json.Serialization;

namespace rotten_potatoes_api.Controllers
{
    public struct Cover
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace BackendAPI.Services
{
    public class GeocodingRespons
    {
        [JsonPropertyName("lat")]
        public string Latitude { get; set; }
        [JsonPropertyName("lon")]
        public string Longitude { get; set; }
    }
}

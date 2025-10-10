using System.Text.Json.Serialization;

namespace BackendAPI.DTO
{
    public class CoordinatesDTO
    {
        [JsonPropertyName("lat")]
        public string Lat {  get; set; }

        [JsonPropertyName("lon")]
        public string Lng { get; set; }
    }
}

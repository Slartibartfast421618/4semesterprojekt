using BackendAPI.DTO;

namespace BackendAPI.Services
{
    public class NominatimGeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;

        //constructor
        public NominatimGeocodingService(HttpClient httpClient) // ift. Troels' - sprunget over Geoclient
        {
            _httpClient = httpClient;
        }

        public async Task<GeocodingRespons> GetCoordinatesAsync (CoordinatesDTO coordinatesDTO) // jeg skal hente fra query som jeg gjorde før 
        {

        }

    }
}

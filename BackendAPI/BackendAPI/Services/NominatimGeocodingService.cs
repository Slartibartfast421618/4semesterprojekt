using BackendAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendAPI.Services
{
    public class NominatimGeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;

        public NominatimGeocodingService(IHttpClientFactory factory) // has to be factory to work
        {
            _httpClient = factory.CreateClient("NominatimAPI");
        }

        public async Task<CoordinatesDTO> GetCoordinatesAsync (string address) 
        {
            string query = $"search?q={address}&format=json&limit=1";

            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(query);

            try
            {
                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var coordinates = JsonSerializer.Deserialize<List<CoordinatesDTO>>(responseContent);
                return coordinates.FirstOrDefault();
            }
            catch
            {
                throw new Exception ("No coordinates");
            }
        }
    }
}

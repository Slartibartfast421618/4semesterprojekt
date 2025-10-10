using BackendAPI.DTO;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BackendAPI.Services
{
    public interface IGeocodingService
    {
        public Task<GeocodingRespons> GetCoordinatesAsync (CoordinatesDTO coordinatesDTO);
    }
}

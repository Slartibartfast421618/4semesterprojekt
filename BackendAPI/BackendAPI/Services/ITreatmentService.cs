using BackendAPI.DTO;

namespace BackendAPI.Services
{
    public interface ITreatmentService
    {
        List<TreatmentDTO> GetDummyTreatments();
        List<TreatmentDTO> GetRandomDummyTreatments(int minCount = 3, int maxCount = 6);
    }
}

namespace BackendAPI.DTO
{
    public class HairdresserWithTreatmentsDTO : HairdresserDTO
    {
        public List<TreatmentDTO> Treatments { get; set; } = new List<TreatmentDTO>();
    }
}

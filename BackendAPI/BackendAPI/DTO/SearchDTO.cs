namespace BackendAPI.DTO
{
    public class SearchDTO
    {
        public CoordinatesDTO coordinates { get; set; }
        public List<HairdresserWithTreatmentsDTO> hairdressers { get; set; }
    }
}

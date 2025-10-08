namespace BackendAPI.Models
{
    public class Hairdresser
    {
        public int ID { get; set; }
        public string SalonName { get; set; }
        public string Website { get; set; }  
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}

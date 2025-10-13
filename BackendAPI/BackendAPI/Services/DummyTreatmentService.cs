using BackendAPI.DTO;

namespace BackendAPI.Services
{
    public class DummyTreatmentService : ITreatmentService
    {
        // randomizer
        private readonly Random _random = new Random();
        
        // dummy treatment types and priceranges
        private readonly Dictionary<string, (decimal min , decimal max)> _treatmentAndPrice = new()
        {
            { "Børneklip" , ( 100 , 300 ) } ,
            { "Dameklip, langt hår" , (600 , 1200) } ,
            { "Dameklip, kort hår" , (300 , 700) } ,
            { "Herreklip" , (100 , 500) } ,
            { "Farve" , (800 , 1400) } ,
            { "Vippe farve" , (50 , 150)}
        };

        // generate 6 dummy treatments for hairdressers 
        public List<TreatmentDTO> GetDummyTreatments ()
        {
            return _treatmentAndPrice.Select( t => new TreatmentDTO
            {
                TreatmentType = t.Key, 
                TreatmentPrice = RandomDecimal ( t.Value.min , t.Value.max )
            }).ToList();
        }

        // generate 3-6 dummy treatments per hairdresser
        public List<TreatmentDTO> GetRandomDummyTreatments( int minCount = 3 , int maxCount = 6)
        {
            int count = _random.Next( minCount , maxCount +1 );

            var shuffled = _treatmentAndPrice.OrderBy(x => _random.Next()).Take(count);

            return shuffled.Select(t => new TreatmentDTO
            {
                TreatmentType = t.Key,
                TreatmentPrice = RandomDecimal(t.Value.min, t.Value.max)
            }).ToList();
        }

        // randomize the price within the range
        private decimal RandomDecimal (decimal min , decimal max)
        {
            return min + (max - min) * (decimal)_random.NextDouble();
                // (decimal)_random.NextDouble() generates a number between 0.0 and 1.0
        }
    }
}

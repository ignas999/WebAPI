namespace WebApi.DataTransferObject
{
    public class LocationsDto
    {
        public int location_id { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public int capacity { get; set; }
        public TimeOnly opens_at { get; set; }
        public TimeOnly closes_at { get; set; }
        
    }
}

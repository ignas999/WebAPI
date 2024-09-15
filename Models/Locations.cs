using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Locations
    {
        [Key]
        public int location_id { get; set; }
        public string street { get; set; } 
        public string city { get; set; }
        public int capacity { get; set; }
        public TimeOnly opens_at { get; set; }
        public TimeOnly closes_at { get; set; }

        // location can have many transports
        public ICollection<Transports> Transports { get; set; }

        //location can have many employees
        public ICollection<Workers> Workers { get; set; }
    }
}

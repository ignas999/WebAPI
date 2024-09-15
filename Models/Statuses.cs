using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Statuses
    {
        [Key]
        public int status_id { get; set; }
        public string name { get; set; }

        // Statuses can have many transports
     
        public ICollection<Transports> Transports { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Categories
    {
        [Key]
        public int category_id { get; set; }

        public string name { get; set; }


        // category can have many transports 
        public ICollection<Transports> Transports { get; set; }
    }
}

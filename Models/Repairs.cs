using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Repairs
    {
        [Key] public int repair_id { get; set; }

        public string name { get; set; }

        // repair can be used on many maintenance records
        public ICollection<Maintenances> Maintenances { get; set; }
    }
}

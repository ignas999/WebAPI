using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Maintenances
    {
        [Key]
        public int maintenance_id { get; set; }

        public int worker_id { get; set; }

        public Workers Worker { get; set; }

        public int transport_id { get; set; }

        public Transports Transport { get; set; }

        public int repair_id { get; set; }

        public Repairs Repair { get; set; }

        public double price { get; set; }

        public int mileage { get; set; }

        public DateOnly date { get; set; }

    }
}

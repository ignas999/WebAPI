using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Orders
    {
        [Key]
        public int order_id { get; set; }

        public int user_id { get; set; }
        public Users User { get; set; }
        public int transport_id { get; set; }
        public Transports Transport { get; set; }
        public int status_id {  get; set; }
        public Statuses Status { get; set; }
        public int amount { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}

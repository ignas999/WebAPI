using WebApi.Models;

namespace WebApi.DataTransferObject
{
    public class OrdersDto
    {
        public int order_id { get; set; }

        public int user_id { get; set; }
        public UsersDto? User { get; set; }
        public int transport_id { get; set; }
        public TransportsDto? Transport { get; set; }
        public int status_id { get; set; }
        public StatusesDto? Status { get; set; }
        public int amount { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}

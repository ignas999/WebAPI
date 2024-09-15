using WebApi.Models;

namespace WebApi.DataTransferObject
{
    public class TransactionsDto
    {
        public int transaction_id { get; set; }

        public int user_id { get; set; }
        public UsersDto? User { get; set; }

        public double amount { get; set; }

        public int worker_id { get; set; }

        public WorkersDto? Worker { get; set; }

        public DateOnly date { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Transactions
    {
        [Key]
        public int transaction_id { get; set; }

        public int user_id { get; set; }
        public Users User { get; set; }

        public double amount { get; set; }

        public int worker_id { get; set; }

        public Workers Worker { get; set; }
        
        public DateOnly date {get; set; }
    }
}

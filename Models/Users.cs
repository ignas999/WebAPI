using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public double balance { get; set; }
        public string? phone { get; set; }

        public ICollection<Orders>Orders { get; set; }
        public ICollection<Transactions> Transactions { get; set; }

        //public ICollection<Ratings> Ratings { get; set; }

    }
}

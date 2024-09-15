using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Workers
    {
        [Key]
        public int worker_id { get; set; }

        public string first_name {  get; set; }

        public string last_name { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public int location_id { get; set; }

        public Locations Location { get; set; }

        public int privillege_id { get; set; }

        public Privilleges Privillege { get; set; }

        //worker can have many maintenances done
        public ICollection<Maintenances> Maintenances { get; set;}

        public ICollection<Transactions> Transactions { get; set;}



    }
}

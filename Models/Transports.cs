using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Transports
    {
        [Key]
        public int transport_id { get; set; }

        //cia yra one relationship tipas , 
        // transport can have only one status
        //we dont need an icollection of <Statuses> because this
        //returns only one object

        public int status_id { get; set; }
        public Statuses Statuses { get; set; }
        public int location_id { get; set; }
        public Locations Locations { get; set; }
        public int category_id { get; set; }
        public Categories Categories { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int rated_power { get; set; }
        public int max_speed { get; set; }
        public int distance {  get; set; }
        public double price { get; set; }

        //transport can have many of these(for relationship purposes):
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Maintenances> Maintenances { get; set;}
    }
}

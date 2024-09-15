using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Privilleges
    {
        [Key] 
        public int privillege_id { get; set; }
        public string name {  get; set; }

        public ICollection<Workers> Workers { get; set; }
    }
}

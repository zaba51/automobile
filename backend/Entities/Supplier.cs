using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }

        public string Name {get; set; }

        public string LogoUrl {get; set; }

        public List<Location> Locations { get; } = new();

        public Supplier(string name)
        {
            Name = name;
        }
    }
}

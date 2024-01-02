using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }
        public string CityName {get; set; }
        public string CountryName {get; set; }
        public List<Supplier> Suppliers { get; } = new();
    }
}
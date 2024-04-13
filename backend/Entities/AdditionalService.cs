using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Models.Shared;

namespace backend.Entities
{
    public class AdditionalService {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public ServiceCategory ServiceCategory { get; set;}

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public List<Reservation> Reservations { get; } = new();
    }
}
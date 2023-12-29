using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Shared;

namespace backend.Entities
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }
        public Company Company {get; set; }
        public string? Name {get; set; }
        public int Power {get; set; }
        
        // [Column(TypeName = "nvarchar(24)")]
        public Gear Gear {get; set; }
        public int DoorCount {get; set; }
        public int SeatCount {get; set; }
        public Engine Engine {get; set; }
        public string? Color {get; set; }
        public string? ImageUrl {get; set; }
    }
}
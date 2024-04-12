using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Models.Shared;

namespace backend.Entities
{
    [Serializable]
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }

        // [JsonConverter(typeof(JsonStringEnumConverter))]
        public int CarCompanyId {get; set; }
        public CarCompany CarCompany {get; set; }

        public string? Name {get; set; }

        public int Power {get; set; }
        
        // [Column(TypeName = "nvarchar(24)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gear Gear {get; set; }

        public int DoorCount {get; set; }

        public int SeatCount {get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Engine Engine {get; set; }
        
        public string? Color {get; set; }
        
        public string? ImageUrl {get; set; }

        // public override bool Equals(object obj)
        // {
        //     if (obj == null)
        //     {
        //         return false;
        //     }
        //     if (!(obj is Model))
        //     {
        //         return false;
        //     }
        //     return (
        //         this.Company == ((Model)obj).Company &&
        //         this.Name == ((Model)obj).Name &&
        //         this.Power == ((Model)obj).Power &&
        //         this.Gear == ((Model)obj).Gear &&
        //         this.DoorCount == ((Model)obj).DoorCount &&
        //         this.SeatCount == ((Model)obj).SeatCount &&
        //         this.Engine == ((Model)obj).Engine &&
        //         this.Color == ((Model)obj).Color
        //         // this.ImageUrl == ((Model)obj).ImageUrl
        //     );
        // }
    }
}
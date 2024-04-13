using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }

        [Required]
        [ForeignKey("CatalogItemId")]
        public CatalogItem CatalogItem {get; set; }
        public int CatalogItemId {get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User {get; set; }
        public int UserId {get; set; }

        [Required]
        public DateTime BeginTime {get; set; }

        [Required]
        public DateTime EndTime {get; set; }

        [Required]
        [ForeignKey("DriversDetailsId")]
        public DriversDetails DriversDetails {get; set; }
        public int DriversDetailsId {get; set; }

        public List<AdditionalService> AdditionalServices { get; } = new();
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class CatalogItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }

        [Required]
        [ForeignKey("ModelId")]
        public Model Model {get; set; }
        public int ModelId {get; set; }

        public double Price {get; set; }

        public string? Supplier {get; set; }
    }
}

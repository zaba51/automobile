using backend.Entities;

namespace automobile.Models
{
    public class AddCatalogItemDTO
    {
        public Model Model {get; set; }
        public int ModelId {get; set; }

        public double Price {get; set; }

        public string? Supplier {get; set; }
    }
}
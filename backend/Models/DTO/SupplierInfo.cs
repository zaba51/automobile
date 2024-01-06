using backend.Entities;

namespace automobile.Models
{
    public class SupplierInfo {
        public Supplier Supplier {get; set; }
        public IEnumerable<CatalogItem> CatalogItems {get; set; }
    }
}
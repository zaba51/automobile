using backend.Entities;

namespace backend.Models
{
    public class ReservationTransactionDTO
    {
        public Guid Guid {get; set; }
        public DateTime TransactionTime {get; set; }

        public CatalogItem CatalogItem {get; set; }

        public int UserId {get; set; }

        public DateTime BeginTime {get; set; }

        public DateTime EndTime {get; set; }
    }
}
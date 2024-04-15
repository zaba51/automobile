using System.Text.Json.Serialization;
using backend.Entities;

namespace backend.Models
{
    public enum TransactionType {
        CANCEL,
        RESERVE
    }

    public class ReservationTransactionDTO
    {
        public Guid Guid {get; set; }
        public DateTime TransactionTime {get; set; }

        public CatalogItem CatalogItem {get; set; }

        public int UserId {get; set; }

        public DateTime BeginTime {get; set; }

        public DateTime EndTime {get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType TransactionType {get; set; }
    }
}
using backend.Entities;

namespace backend.Models
{
    public class AddReservationDTO
    {
        public int CatalogItemId {get; set; }

        public int UserId {get; set; }

        public DateTime BeginTime {get; set; }

        public DateTime EndTime {get; set; }

        public DriversDetails DriversDetails {get; set; }

        public List<int> AdditionalServiceIds {get; set; } = new();
    }
}
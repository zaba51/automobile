using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    public class DriversDetails
    {
        public string Name {get; set; }
        public string Surname {get; set; }
        public string Country {get; set; }
        public string Number {get; set; }
    }
}
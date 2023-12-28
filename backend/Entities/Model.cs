namespace backend.Entities
{
    public class Model
    {
        public int Id {get; set; }
        public string? Name {get; set; }
        public int Power {get; set; }
        public string? Gear {get; set; }
        public int DoorCount {get; set; }
        public int SeatCount {get; set; }
        public string? Engine {get; set; }
        public string? Color {get; set; }
        public string? ImageUrl {get; set; }
    }
}
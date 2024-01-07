namespace backend.models.DTO
{
    public class AddItemDto
    {
        public string NewItem { get; set; }
        public IFormFile File { get; set; }
    }
}

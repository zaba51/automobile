namespace backend.Services {
    public interface IUploadService {
        public Task<string> Upload(IFormFile file, string[] paths);
    }
}
namespace backend.Services {
    public class UploadService {
        public static async Task<string> Upload(IFormFile file, string[] paths) {
            string uploadsFolder = Path.Combine(paths);
            
             if (file != null && file.Length > 0)
                {
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string[] relativeFilePaths = 
                        paths.Where((_, i) => i != 0).Append(uniqueFileName).ToArray();
                        
                    return Path.Combine(relativeFilePaths);
                }
            else {
                throw new FileNotFoundException();
            }
        }
    }
}
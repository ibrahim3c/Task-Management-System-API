using MyShop.Services.Interfaces;

namespace MyShop.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteFileAsync(string FilePath)
        {
            // => /wwwroot/
            //var fullPath = Path.Combine(webHostEnvironment.WebRootPath, FilePath);
            var fullPath = webHostEnvironment.WebRootPath+"/" +FilePath;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);



            }
        }




        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            try
            {

                if (file == null || file.Length == 0) return string.Empty;


                //var path = Path.Combine(webHostEnvironment.WebRootPath, folder);
                var path = webHostEnvironment.WebRootPath+"/"+ folder;
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid().ToString()}{extension}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fullPath = Path.Combine(path, fileName);

                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Flush();
                }

                return Path.Combine(folder, fileName).Replace("\\", "/"); // Return relative path
            }
            catch
            {
                return string.Empty;
            }
        }

        // New function to retrieve image as an IFormFile
        public async Task<IFormFile> GetFileAsIFormFileAsync(string imageSrc)
        {
            try
            {
                var fullPath = webHostEnvironment.WebRootPath + "/" + imageSrc;
                if (!File.Exists(fullPath))
                {
                    return null; // Return null if the file doesn't exist
                }

                // Read the file into a byte array or stream
                var memoryStream = new MemoryStream(await File.ReadAllBytesAsync(fullPath));

                // Create an IFormFile from the MemoryStream
                IFormFile formFile = new FormFile(memoryStream, 0, memoryStream.Length, "profilePicture", Path.GetFileName(fullPath))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg" // Set content type (change based on file type)
                };

                return formFile;
            }
            catch
            {
                return null; // Return null in case of an error
            }
        }

    }
}

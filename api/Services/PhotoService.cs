using api.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;


namespace api.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<PhotoService> _logger;
        public PhotoService(IWebHostEnvironment webHostEnvironment, ILogger<PhotoService> logger)
        {
            _webHostEnvironment=webHostEnvironment;
            _logger=logger;
        }
        public async Task<string> SavePhotoAsync(IFormFile file, string folder = "uploads")
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogWarning("No file uploaded or file is empty.");
                    return null;
                }

                _logger.LogInformation("Starting to save file. File name: {FileName}, Folder: {Folder}", file.FileName, folder);

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                // Check if the directory exists, if not, create it
                if (!Directory.Exists(uploadsFolder))
                {
                    _logger.LogInformation("Directory {UploadsFolder} does not exist, creating directory.", uploadsFolder);
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                _logger.LogInformation("Saving file at: {FilePath}", filePath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                _logger.LogInformation("File successfully saved at {FilePath}", filePath);

                return $"{folder}/{uniqueFileName}"; // static file path
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the photo. Exception: {ExceptionMessage}", ex.Message);
                throw new Exception("An error occurred while saving the photo.", ex);
            }
        }

    }
}

namespace api.Interfaces
{
    public interface IPhotoService
    {
        Task<string> SavePhotoAsync(IFormFile file, string folder = "uploads");

    }
}

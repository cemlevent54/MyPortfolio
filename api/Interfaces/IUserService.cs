namespace api.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserRole(string id);
    }
}

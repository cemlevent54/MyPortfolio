using api.Models;

namespace api.Interfaces
{
    public interface IUserInterface
    {
        // get all users
        public Task<IEnumerable<AppUsers>> GetUsers();

        // get user by id
        public Task<AppUsers> GetUserById(AppUsers user);

        public Task<AppUsers> GetUserByFId(string id);

        // create user
        public Task<AppUsers> CreateUser(AppUsers user);

        // update user
        public Task<AppUsers> UpdateUser(AppUsers user);

        // delete user
        public Task<AppUsers> DeleteUser(AppUsers user);
    }
}

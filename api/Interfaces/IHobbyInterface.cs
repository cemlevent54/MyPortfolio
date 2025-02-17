using api.Models;

namespace api.Interfaces
{
    public interface IHobbyInterface
    {
        Task<IEnumerable<Hobbies>> GetHobbies();
        Task<Hobbies> GetHobby(int id);
        Task<Hobbies> AddHobby(Hobbies hobby);
        Task<Hobbies> UpdateHobby(Hobbies hobby);
        Task<Hobbies> DeleteHobby(Hobbies hobby);

        // hobbies and users has many to many relationship. I want to add a user to a hobby, and remove a user from a hobby
        Task<Hobbies> AddUserToHobby(int hobby_id, string user_id);
        Task<Hobbies> RemoveUserFromHobby(int hobby_id, string user_id);

        // hobbies and users has many to many relationship. I want to get all users of a hobby
        Task<IEnumerable<AppUsers>> GetUsersOfHobby(int hobby_id);

        // hobbies and users has many to many relationship. I want to get all hobbies of a user
        Task<IEnumerable<Hobbies>> GetHobbiesOfUser(string user_id);


    }
}

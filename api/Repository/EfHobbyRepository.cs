using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfHobbyRepository : IHobbyInterface
    {
        private readonly ApplicationDbContext _context;
        public EfHobbyRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Hobbies>> GetHobbies()
        {
            return await _context.Hobbies.ToListAsync();
        }

        public async Task<Hobbies> GetHobby(int id)
        {
            return await _context.Hobbies.FindAsync(id);
        }

        public async Task<Hobbies> AddHobby(Hobbies hobby)
        {
            _context.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();
            return hobby;
        }

        public async Task<Hobbies> UpdateHobby(Hobbies hobby)
        {
            _context.Hobbies.Update(hobby);
            await _context.SaveChangesAsync();
            return hobby;
        }

        public async Task<Hobbies> DeleteHobby(Hobbies hobby)
        {
            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();
            return hobby;
        }

        public async Task<Hobbies> AddUserToHobby(int hobby_id, string user_id)
        {
            var hobby = await _context.Hobbies.Include(x => x.AppUsers).FirstOrDefaultAsync(x => x.Hobby_ID == hobby_id);
            var user = await _context.AppUsers.FindAsync(user_id);
            if (hobby != null && user != null)
            {
                hobby.AppUsers.Add(user);
                await _context.SaveChangesAsync();
            }
            return hobby;
        }

        public async Task<Hobbies> RemoveUserFromHobby(int hobby_id, string user_id)
        {
            var hobby = await _context.Hobbies.Include(x => x.AppUsers).FirstOrDefaultAsync(x => x.Hobby_ID == hobby_id);
            var user = await _context.AppUsers.FindAsync(user_id);
            if (hobby != null && user != null)
            {
                hobby.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
            return hobby;
        }

        public async Task<IEnumerable<AppUsers>> GetUsersOfHobby(int hobby_id)
        {
            var hobby = await _context.Hobbies
                .Include(h => h.AppUsers)  // Hobi ile ilişkili kullanıcıları yükle
                .FirstOrDefaultAsync(h => h.Hobby_ID == hobby_id); // Verilen hobby_id'ye sahip hobiyi bul

            if (hobby == null)
            {
                return null; // Hobi bulunamazsa null döndür
            }

            return hobby.AppUsers; // Hobiye bağlı tüm kullanıcıları döndür
        }


        public async Task<IEnumerable<Hobbies>> GetHobbiesOfUser(string user_id)
        {
            var user = await _context.AppUsers
                .Include(u => u.Hobbies)  // Kullanıcı ile ilişkili hobileri yükle
                .FirstOrDefaultAsync(u => u.Id == user_id); // Verilen user_id'ye sahip kullanıcıyı bul

            if (user == null)
            {
                return null; // Kullanıcı bulunamazsa null döndür
            }

            return user.Hobbies; // Kullanıcıya ait tüm hobileri döndür
        }

    }
}

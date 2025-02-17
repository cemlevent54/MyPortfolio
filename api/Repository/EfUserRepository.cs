using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.Extensions.Primitives;

namespace api.Repository
{
    public class EfUserRepository : IUserInterface
    {
        private readonly ApplicationDbContext _context;
        public EfUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AppUsers> CreateUser(AppUsers user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<AppUsers> DeleteUser(AppUsers user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.AppUsers.Remove(user);
            return Task.FromResult(user);
        }


        public async Task<AppUsers> GetUserByFId(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var user = await _context.AppUsers.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            return user;
        }

        public Task<IEnumerable<AppUsers>> GetUsers()
        {
            return Task.FromResult(_context.AppUsers.AsEnumerable());
        }

        public Task<AppUsers> UpdateUser(AppUsers user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.AppUsers.Update(user);
            return Task.FromResult(user);
        }

        public Task<AppUsers> GetUserById(AppUsers user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user);
        }
    }
}

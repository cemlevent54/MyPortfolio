using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfLanguageRepository : ILanguageInterface
    {
        private readonly ApplicationDbContext _context;
        public EfLanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ForeignLanguages> AddLanguage(ForeignLanguages language)
        {
            _context.ForeignLanguages.Add(language);
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task<ForeignLanguages> AddUserToLanguage(int lang_id, string user_id)
        {
            var language = await _context.ForeignLanguages.Include(x => x.AppUsers).FirstOrDefaultAsync(x => x.Language_ID == lang_id);
            var user = await _context.AppUsers.FindAsync(user_id);
            if (language != null && user != null)
            {
                language.AppUsers.Add(user);
                await _context.SaveChangesAsync();
            }
            return language;
        }

        public async Task<ForeignLanguages> DeleteLanguage(ForeignLanguages language)
        {
            _context.ForeignLanguages.Remove(language);
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task<ForeignLanguages> GetLanguageById(int id)
        {
            return await _context.ForeignLanguages.FindAsync(id);
        }

        public async Task<IEnumerable<ForeignLanguages>> GetLanguages()
        {
            return await _context.ForeignLanguages.ToListAsync();
        }

        public async Task<IEnumerable<ForeignLanguages>> GetLanguagesOfUser(string user_id)
        {
            var user = await _context.AppUsers.Include(x => x.ForeignLanguages).FirstOrDefaultAsync(x => x.Id == user_id);
            if(user == null)
            {
                return null;
            }
            return user.ForeignLanguages;

        }

        public async Task<IEnumerable<AppUsers>> GetUsersOfLanguages(int lang_id)
        {
            var language = await _context.ForeignLanguages.Include(x => x.AppUsers).FirstOrDefaultAsync(x => x.Language_ID == lang_id);
            if(language == null)
            {
                return null;
            }
            return language.AppUsers;

        }

        public async Task<ForeignLanguages> RemoveUserFromLanguage(int lang_id, string user_id)
        {
            var language = await _context.ForeignLanguages.Include(x => x.AppUsers).FirstOrDefaultAsync(x => x.Language_ID == lang_id);
            var user = await _context.AppUsers.FindAsync(user_id);
            if (language != null && user != null)
            {
                language.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
            return language;
        }

        public async Task<ForeignLanguages> UpdateLanguage(ForeignLanguages language)
        {
            _context.ForeignLanguages.Update(language);
            await _context.SaveChangesAsync();
            return language;
        }
    }
}

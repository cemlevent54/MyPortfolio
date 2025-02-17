using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.Repository
{
    public class EfTechnologyRepository : ITechnologyInterface
    {
        private readonly ApplicationDbContext _context;
        public EfTechnologyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Technologies> AddTechnology(Technologies technology)
        {
            _context.Technologies.Add(technology);
            await _context.SaveChangesAsync();
            return technology;

        }

        public async Task<Technologies> DeleteTechnology(Technologies technology)
        {
            var tech = await _context.Technologies.FindAsync(technology.Technology_ID);
            if (tech == null)
            {
                return null;
            }
            _context.Technologies.Remove(tech);
            await _context.SaveChangesAsync();
            return tech;
        }

        public async Task<IEnumerable<Technologies>> GetTechnologies()
        {
            return await _context.Technologies.ToListAsync();
        }

        public async Task<Technologies> GetTechnology(int id)
        {
            return await _context.Technologies.FindAsync(id);
        }

        public async Task<Technologies> UpdateTechnology(Technologies technology)
        {
            _context.Technologies.Update(technology);
            await _context.SaveChangesAsync();
            return technology;
        }
    }
}

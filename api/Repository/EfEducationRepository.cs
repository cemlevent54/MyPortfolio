using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfEducationRepository : IEducationInterface
    {
        private readonly ApplicationDbContext _context;
        public EfEducationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Educations> CreateEducation(Educations education)
        {
            if (education == null)
            {
                throw new ArgumentNullException(nameof(education));
            }
            _context.Educations.Add(education);
            await _context.SaveChangesAsync();
            return education;
        }

        public async Task<Educations> DeleteEducation(Educations education)
        {
            if (education == null)
            {
                throw new ArgumentNullException(nameof(education));
            }
            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            return education;
        }

        public async Task<Educations> GetEducationById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var education = await _context.Educations.FindAsync(id);
            return education;
        }

        public async Task<IEnumerable<Educations>> GetEducations()
        {
            return await _context.Educations.Include(e => e.AppUsers).Include(e => e.EducationCategory).ToListAsync();
        }

        public async Task<Educations> UpdateEducation(Educations education)
        {
            if (education == null)
            {
                throw new ArgumentNullException(nameof(education));
            }
            _context.Educations.Update(education);
            await _context.SaveChangesAsync();
            return education;
        }
    }
}

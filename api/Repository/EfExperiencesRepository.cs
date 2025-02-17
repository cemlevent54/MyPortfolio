using api.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfExperiencesRepository : IExperienceInterface
    {
        private readonly ApplicationDbContext _context;
        public EfExperiencesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Experiences> CreateExperience(Experiences experience)
        {
            if(experience == null)
            {
                throw new ArgumentNullException(nameof(experience));
            }
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();
            return experience;
        }

        public async Task<Experiences> DeleteExperience(Experiences experience)
        {
            if(experience == null)
            {
                throw new ArgumentNullException(nameof(experience));
            }
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return experience;
        }

        public async Task<Experiences> GetExperienceById(int id)
        {
            if(id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var experience = await _context.Experiences.FindAsync(id);
            return experience;
        }

        public async Task<IEnumerable<Experiences>> GetExperiences()
        {
            return await _context.Experiences.Include(e => e.AppUsers).ToListAsync();
        }

        public async Task<Experiences> UpdateExperience(Experiences experience)
        {
            if(experience == null)
            {
                throw new ArgumentNullException(nameof(experience));
            }
            _context.Experiences.Update(experience);
            _context.SaveChangesAsync();
            return experience;
        }
    }
}

using api.Data;
using api.DTOs.EducationCategory;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfEducationCategoryRepository : IEducationCategoryInterface
    {
        private readonly ApplicationDbContext _context;

        public EfEducationCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EducationCategory> CreateEducationCategory(EducationCategory educationCategory)
        {
            if (educationCategory.EducationCategory_Name == null)
                throw new ArgumentNullException(nameof(educationCategory));

            _context.EducationCategories.Add(educationCategory);
            await _context.SaveChangesAsync();
            return educationCategory;
        }

        public async Task<IEnumerable<EducationCategory>> GetEducationCategories()
        {
            return await _context.EducationCategories.ToListAsync();
        }

        public async Task<EducationCategory> DeleteEducationCategory(EducationCategory educationCategory)
        {
            if (educationCategory == null)
                throw new ArgumentNullException(nameof(educationCategory));

            _context.EducationCategories.Remove(educationCategory);
            await _context.SaveChangesAsync();
            return educationCategory;
        }

        public async Task<EducationCategory> UpdateEducationCategory(EducationCategory educationCategory)
        {
            if (educationCategory == null)
                throw new ArgumentNullException(nameof(educationCategory));

            _context.EducationCategories.Update(educationCategory);
            await _context.SaveChangesAsync();
            return educationCategory;
        }

        public async Task<EducationCategory> GetEducationCategoryById(int id)
        {
            var educationCategory = await _context.EducationCategories.FindAsync(id);
            return educationCategory;
        }
    }
}

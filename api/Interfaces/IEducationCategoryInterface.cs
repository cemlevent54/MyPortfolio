using api.DTOs.EducationCategory;
using api.Models;

namespace api.Interfaces
{
    public interface IEducationCategoryInterface
    {
        Task<EducationCategory> CreateEducationCategory(EducationCategory educationCategory);
        Task<IEnumerable<EducationCategory>> GetEducationCategories();
        Task<EducationCategory> UpdateEducationCategory(EducationCategory educationCategory);
        Task<EducationCategory> DeleteEducationCategory(EducationCategory educationCategory);

        Task<EducationCategory> GetEducationCategoryById(int id);
    }
}

using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class EducationCategory
    {
        [Key]
        public int EducationCategory_ID { get; set; }
        public string EducationCategory_Name { get; set; }
        // relationship between EducationCategory and Educations will be one to many, one category has many educations
        // but one education belongs to one category
        public ICollection<Educations> Educations { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class ProjectCategory
    {
        [Key]
        public int ProjectCategory_ID { get; set; }

        public string? ProjectCategory_Name { get; set; }

        public ICollection<Projects> Projects { get; set; } = new List<Projects>();
    }
}

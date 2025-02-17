using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // ICollection için

namespace api.Models
{
    public class Technologies
    {
        [Key]
        public int Technology_ID { get; set; }
        public string? Technology_Name { get; set; }
        public string? Technology_IconUrl { get; set; }

        // relationship between Technologies and Projects will be many to many
        public ICollection<Projects> Projects { get; set; }
    }
}

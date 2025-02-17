using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Collections.Generic; // ICollection için
using System.ComponentModel.DataAnnotations; // Key ve diğer öznitelikler için

namespace api.Models
{
    public class Hobbies
    {
        [Key]
        public int Hobby_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Hobby_Name { get; set; }
        public string? Hobby_IconUrl { get; set; }
        // relationship between AppUsers and Hobbies will be many to many
        public ICollection<AppUsers> AppUsers { get; set; }
    }
}

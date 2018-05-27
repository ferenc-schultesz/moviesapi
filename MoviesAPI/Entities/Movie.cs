using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Entities
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [Range(1895, int.MaxValue)]
        public int YearOfRelease { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int RunningTime { get; set; }

        public ICollection<Rating> Ratings { get; set; }
            = new List<Rating>();

        public string About { get; set; }
        public string Category { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Entities
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public int UserRating { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime RatedOn { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
    }
}

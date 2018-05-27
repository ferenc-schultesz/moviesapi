using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class RatingCreationDto
    {
        [Required(ErrorMessage = "Rating should be integer [1..5]")]
        [Range(1,5)]
        public int UserRating { get; set; }

        [Required(ErrorMessage = "Provide a UserId")]
        public int UserId { get; set; }
    }
}

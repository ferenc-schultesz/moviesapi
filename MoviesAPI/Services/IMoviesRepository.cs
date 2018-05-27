using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IMoviesRepository
    {
        IQueryable<Movie> GetMovies();
        IQueryable<Rating> GetRatings();
        IQueryable<User> GetUsers();
        void AddRating(Rating r);
        void UpdateRating(Rating r);


    }
}

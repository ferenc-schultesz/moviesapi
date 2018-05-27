using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.Entities;

namespace MoviesAPI.Services
{
    public class MoviesRepository : IMoviesRepository
    {
        private MoviesContext context;

        public MoviesRepository (MoviesContext _context)
        {
            context = _context;
        }

        public void AddRating(Rating r)
        {
            context.Ratings.Add(r);
            context.SaveChanges();
        }

        public IQueryable<Movie> GetMovies()
        {
            return context.Movies;
        }

        public IQueryable<Rating> GetRatings()
        {
            return context.Ratings;
        }

        public void UpdateRating(Rating r)
        {
            context.Update(r);
            context.SaveChanges();
        }
        public IQueryable<User> GetUsers()
        {
            return context.Users;
        }
    }
}

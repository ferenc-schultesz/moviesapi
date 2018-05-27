using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }
        /// <summary>
        /// Returns a list of movies or a movie based on the filter parameters. Minimum one filter is mandatory.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="year"></param>
        /// <param name="genre"></param>
        /// <returns></returns>        
        // GET api/values
        [HttpGet]
        public  IActionResult GetMovies(string title = null, int? year = null, string genre = null)
        {
            if (title == null && genre == null && year == null)
            {
                return BadRequest();
            }

            var movies =_moviesRepository.GetMovies().OrderBy(m => m.Title);
            if (movies == null)
            {
                return NotFound();
            }

            IQueryable<Movie> _movies =  movies;

            if (title != null)
            {
                _movies = _movies.Where(m => m.Title.Contains(title));
            }

            if (year != null)
            {
                _movies = _movies.Where(r => r.YearOfRelease == year);
            }

            if (genre != null)
            {
                _movies = _movies.Where(r => r.Category.ToLower() == genre.ToLower());
            }

            if (_movies.Count() == 0)
            {
                return NotFound();
            }

            var results = convertToMoviesDto(_movies);
            
            return Ok(results);
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    var movie = from _movies in _ctx.Movies
        //                 where _movies.Id == id
        //                 select _movies;

        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(new JsonResult(movie));
        //}

        //[HttpGet("{movieId}/ratings/{id}", Name = "GetRating")]
        //public IActionResult GetRating(Guid movieId, Guid id)
        //{
        //    var movie = (from _movies in _ctx.Movies
        //                 where _movies.Id == movieId
        //                 select _movies).FirstOrDefault();

        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    var rating = (from _ratings in _ctx.Ratings
        //                 where _ratings.Id == movieId
        //                 select _ratings).FirstOrDefault();

        //    if (rating == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(rating);
        //}

        /// <summary>
        /// Returns the top 5 movies based on acerage user reviews
        /// </summary>
        /// <returns></returns>
        [HttpGet("top5")]
        public IActionResult GetTop5()
        {
            var topMovies = _moviesRepository.GetMovies().OrderByDescending(m => m.Ratings.Average(r => r.UserRating)).Take(5);

            if (topMovies == null)
            {
                return NotFound();
            }

            var results = convertToMoviesDto(topMovies);
            return Ok(results);
        }
        /// <summary>
        /// Returns the top 5 movies based on a user's reviews. User's id is mandatory
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("top5/{userId}")]
        public IActionResult GetTop5ByUser(int userId)
        {
            var user = (from users in _moviesRepository.GetUsers()
                        where users.Id == userId
                        select users).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            var topMovies = (from movies in _moviesRepository.GetMovies()
                             join ratings in _moviesRepository.GetRatings() on movies.Id equals ratings.MovieId
                             orderby ratings.UserRating descending, movies.Title ascending
                             where ratings.UserId == userId
                             select movies).Take(5);

            if (topMovies == null)
            {
                return NotFound();
            }

            var results = convertToMoviesDto(topMovies);

            return Ok(results);
        }


        /// <summary>
        /// Adds a new review for a movie, if the user already had one, it updates the existing.
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        // POST api/values
        [HttpPost("{movieId}/rating")]
        public IActionResult AddRating(Guid movieId, [FromBody]  RatingCreationDto rating)
        {
            if (rating == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = (from users in _moviesRepository.GetUsers()
                        where users.Id == rating.UserId
                        select users).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            var movie = (from _movies in _moviesRepository.GetMovies()
                         where _movies.Id == movieId
                        select _movies).FirstOrDefault();

            if (movie == null)
            {
                return NotFound();
            }

            var oldRating = (from _ratings in _moviesRepository.GetRatings()
                             where _ratings.MovieId == movieId && _ratings.UserId == rating.UserId
                            select _ratings).SingleOrDefault();
          
            if (oldRating != null)
            {
                oldRating.UserRating = rating.UserRating;
                oldRating.RatedOn = DateTime.Today;
                _moviesRepository.UpdateRating(oldRating);
                return Ok();
            }

            var newRating = new Rating()
            {
                UserRating = rating.UserRating,
                UserId = rating.UserId,
                RatedOn = DateTime.Today,
                MovieId = movie.Id
            };
            _moviesRepository.AddRating(newRating);

            return Ok();
        }

       
        private IEnumerable<MovieDto> convertToMoviesDto(IQueryable<Movie> movies)
        {
        
            var results = Mapper.Map<IEnumerable<MovieDto>>(movies);
            foreach (MovieDto result in results)
            {
                double avg = _moviesRepository.GetRatings().Where(ra => ra.MovieId == result.Id).Average(r => r.UserRating);
                result.AverageRating = (float)Math.Round(2 * avg) / 2;
            }
            return results;
        }
    }
}

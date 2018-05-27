using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI
{
    public static class MoviesContextExtension
    {
        public static void EnsureSeedDataForContext(this MoviesContext context)
        {
            if (context.Movies.Any())
            {
                return;
            }

            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = $"Die Hard",
                    YearOfRelease = 2000,
                    RunningTime = 110,
                    About = $"Just another action movie",
                    Category = $"Action",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 4,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 2,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 2,
                            UserRating = 1,
                            RatedOn = DateTime.Today
                        }
                    }
                },

                new Movie()
                {
                    Title = $"Live Easy",
                    YearOfRelease = 2010,
                    RunningTime = 99,
                    About = $"Just another romantic movie",
                    Category = $"Romantic",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 5,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 2,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 2,
                            UserRating = 4,
                            RatedOn = DateTime.Today
                        }
                    }
                },
                new Movie()
                {
                    Title = $"Sharknado",
                    YearOfRelease = 2001,
                    RunningTime = 130,
                    About = $"Movie about sharks and tornados",
                    Category = $"Scifi",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 2,
                            RatedOn = DateTime.Today.AddDays(-5)
                        },

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 1,
                            RatedOn = DateTime.Today.AddDays(-2)
                        }
                    }
                },

                new Movie()
                {
                    Title = $"Star Wars",
                    YearOfRelease = 1980,
                    RunningTime = 122,
                    About = $"Spaceships, jedis and the dark lord",
                    Category = $"Scifi",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 4,
                            RatedOn = DateTime.Today.AddDays(-10)
                        },

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 5,
                            RatedOn = DateTime.Today.AddDays(-13)
                        },

                        new Rating()
                        {
                            UserId = 2,
                            UserRating = 4,
                            RatedOn = DateTime.Today.AddDays(-20)
                        },
                        new Rating()
                        {
                            UserId = 3,
                            UserRating = 5,
                            RatedOn = DateTime.Today.AddDays(-40)
                        }
                    }
                },

                new Movie()
                {
                    Title = $"Die Hard 2",
                    YearOfRelease = 2003,
                    RunningTime = 111,
                    About = $"Just another action movie 2",
                    Category = $"Action",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 3,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 5,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 2,
                            UserRating = 5,
                            RatedOn = DateTime.Today
                        }
                    }
                },
                new Movie()
                {
                    Title = $"Die Hard 3",
                    YearOfRelease = 2010,
                    RunningTime = 114,
                    About = $"Just another action movie 3",
                    Category = $"Action",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 4,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 2,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 2,
                            UserRating = 1,
                            RatedOn = DateTime.Today
                        }
                    }
                },

                new Movie()
                {
                    Title = $"Live Easy 2",
                    YearOfRelease = 2012,
                    RunningTime = 123,
                    About = $"Just another romantic movie 2",
                    Category = $"Romantic",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 5,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 2,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 2,
                            UserRating = 4,
                            RatedOn = DateTime.Today
                        }
                    }
                },
                new Movie()
                {
                    Title = $"Love all around",
                    YearOfRelease = 1999,
                    RunningTime = 89,
                    About = $"Love, love, love",
                    Category = $"Romantic",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 2,
                            RatedOn = DateTime.Today.AddDays(-5)
                        },

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 4,
                            RatedOn = DateTime.Today.AddDays(-2)
                        },
                        new Rating()
                        {
                            UserId = 4,
                            UserRating = 2,
                            RatedOn = DateTime.Today.AddDays(-5)
                        },

                        new Rating()
                        {
                            UserId = 7,
                            UserRating = 4,
                            RatedOn = DateTime.Today.AddDays(-2)
                        }
                    }
                },

                new Movie()
                {
                    Title = $"Xtro",
                    YearOfRelease = 1976,
                    RunningTime = 101,
                    About = $"Ancient terrible movie",
                    Category = $"Scifi",
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            UserId = 0,
                            UserRating = 2,
                            RatedOn = DateTime.Today.AddDays(-10)
                        },

                        new Rating()
                        {
                            UserId = 7,
                            UserRating = 1,
                            RatedOn = DateTime.Today.AddDays(-13)
                        },

                        new Rating()
                        {
                            UserId = 8,
                            UserRating = 2,
                            RatedOn = DateTime.Today.AddDays(-20)
                        },
                        new Rating()
                        {
                            UserId = 4,
                            UserRating = 4,
                            RatedOn = DateTime.Today.AddDays(-40)
                        }
                    }
                },

                new Movie()
                {
                    Title = $"The drunken sailor",
                    YearOfRelease = 2013,
                    RunningTime = 92,
                    About = $"Some random comedy",
                    Category = $"Comedy",
                    Ratings = new List<Rating>()
                    {

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 5,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 2,
                            UserRating = 5,
                            RatedOn = DateTime.Today
                        },
                        new Rating()
                        {
                            UserId = 5,
                            UserRating = 3,
                            RatedOn = DateTime.Today
                        },
                        new Rating()
                        {
                            UserId = 7,
                            UserRating = 3,
                            RatedOn = DateTime.Today
                        }
                    }
                },
                new Movie()
                {
                    Title = $"The drunken sailor 2",
                    YearOfRelease = 2014,
                    RunningTime = 92,
                    About = $"Some random comedy 2",
                    Category = $"Comedy",
                    Ratings = new List<Rating>()
                    {

                        new Rating()
                        {
                            UserId = 1,
                            UserRating = 3,
                            RatedOn = DateTime.Today
                        },

                        new Rating()
                        {
                            UserId = 2,
                            UserRating = 4,
                            RatedOn = DateTime.Today
                        },
                        new Rating()
                        {
                            UserId = 5,
                            UserRating = 2,
                            RatedOn = DateTime.Today
                        },
                        new Rating()
                        {
                            UserId = 7,
                            UserRating = 2,
                            RatedOn = DateTime.Today
                        }
                    }
                }
            };

            // Adding users
            var users = new List<User>()
            {
                new User()
                {
                    Name = $"Ben Big"
                },
                new User()
                {
                    Name = $"Mark Small"
                },
                new User()
                {
                    Name = $"John Red"
                },
                new User()
                {
                    Name = $"Matthew Fat"
                },
                new User()
                {
                    Name = $"Rob Muller"
                },
                new User()
                {
                    Name = $"Ingela Nice"
                },
                new User()
                {
                    Name = $"Vladyslav North"
                },
                new User()
                {
                    Name = $"Tejus Warm"
                },
                new User()
                {
                    Name = $"Martin Good"
                },
                new User()
                {
                    Name = $"Mirealls Green"
                },

            };

            context.Movies.AddRange(movies);
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}

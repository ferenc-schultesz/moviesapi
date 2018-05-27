using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoviesAPI.Controllers;
using MoviesAPI.Entities;
using MoviesAPI.Models;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class MovieControllerTest : IClassFixture<InitFixture>
    {
        InitFixture _fixture;

        public MovieControllerTest(InitFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void GetMovies_WithoutFilters()
        {
            // Arrange done in fixture class

            //Act
            var result = _fixture.moviesController.GetMovies();
            var actionResult = result as IActionResult;

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void GetMovies_WithSingleFilter_MovieDoesNotExists()
        {
            // Arrange done in fixture class

            //Act
            var result = _fixture.moviesController.GetMovies("Die Hard 5");
            var actionResult = result as IActionResult;
            var expected = new NotFoundResult();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetMovies_WithTwoFilters_MovieDoesNotExists()
        {
            // Arrange done in fixture class

            //Act
            var result = _fixture.moviesController.GetMovies("Die Hard", 1500);
            var actionResult = result as IActionResult;
            var expected = new NotFoundResult();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetMovies_WithThreeFilters_MovieDoesNotExists()
        {
            // Arrange done in fixture class

            //Act
            var result = _fixture.moviesController.GetMovies("Die Hard", 2000, "Comedy");
            var actionResult = result as IActionResult;
            var expected = new NotFoundResult();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetMovies_WithSingleFilter_MovieExists()
        {
            // Arrange
            var movies = new List<MovieDto>()
            {
                new MovieDto()
                {
                    Id = new Guid("D3CE3D3B-D3D7-409B-6FC3-08D5C3128E9D"),
                    Title = $"Die Hard",
                    YearOfRelease = 2000,
                    RunningTime = 110,
                    AverageRating = 1.5
                },
                new MovieDto()
                {
                    Id = new Guid("59A12EA1-EA8F-4F5F-6FC7-08D5C3128E9D"),
                    Title = $"Die Hard 2",
                    YearOfRelease = 2003,
                    RunningTime = 111,
                    AverageRating = 4
                },
                new MovieDto()
                {
                    Id = new Guid("0A7B5831-6D55-49DD-6FC8-08D5C3128E9D"),
                    Title = $"Die Hard 3",
                    YearOfRelease = 2010,
                    RunningTime = 114,
                    AverageRating = 2

                }
            };

            //Act
            var result = _fixture.moviesController.GetMovies("Die Hard");
            var okObjectResult = result as OkObjectResult;
            var expected = new OkObjectResult(movies);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnMovie = Assert.IsType<List<MovieDto>>(okResult.Value);
            Assert.Equal(movies.Count, returnMovie.Count);
        }

        [Fact]
        public void GetMovies_WithDoubleFilters_MovieExists()
        {
            // Arrange
            var movies = new List<MovieDto>()
            {
                new MovieDto()
                {
                    Id = new Guid("59A12EA1-EA8F-4F5F-6FC7-08D5C3128E9D"),
                    Title = $"Die Hard 2",
                    YearOfRelease = 2003,
                    RunningTime = 111,
                    AverageRating = 4
                }
            };

            //Act
            var result = _fixture.moviesController.GetMovies("Die Hard", 2003);
            var okObjectResult = result as OkObjectResult;
            var expected = new OkObjectResult(movies);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnMovie = Assert.IsType<List<MovieDto>>(okResult.Value);
            Assert.Equal(movies.Count, returnMovie.Count);
        }

        [Fact]
        public void GetMovies_WithThreeFilters_MovieExists()
        {
            // Arrange
            var movies = new List<MovieDto>()
            {
                new MovieDto()
                {
                    Id = new Guid("59A12EA1-EA8F-4F5F-6FC7-08D5C3128E9D"),
                    Title = $"Die Hard 2",
                    YearOfRelease = 2003,
                    RunningTime = 111,
                    AverageRating = 4
                }
            };

            //Act
            var result = _fixture.moviesController.GetMovies("Die Hard", 2003, "Action");
            var okObjectResult = result as OkObjectResult;
            var expected = new OkObjectResult(movies);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnMovie = Assert.IsType<List<MovieDto>>(okResult.Value);
            Assert.Equal(movies.Count, returnMovie.Count);
        }
    }

    public class InitFixture
    {
        public InitFixture()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Movie, MovieDto>();
            });

            mockSet = new Mock<DbSet<Movie>>();
            mockContext = new Mock<MoviesContext>();
            mockRepository = new Mock<IMoviesRepository>();
            mockRepository = SetupMockRepository(mockRepository);
            moviesController = new MoviesController(mockRepository.Object);

        }
        public Mock<IMoviesRepository> SetupMockRepository(Mock<IMoviesRepository> repo)
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Id = new Guid("D3CE3D3B-D3D7-409B-6FC3-08D5C3128E9D"),
                    Title = $"Die Hard",
                    YearOfRelease = 2000,
                    RunningTime = 110,
                    About = $"Just another action movie",
                    Category = $"Action"
                },

                new Movie()
                {
                    Id = new Guid("03EDB3C7-A2AE-4B27-6FC4-08D5C3128E9D"),
                    Title = $"Live Easy",
                    YearOfRelease = 2010,
                    RunningTime = 99,
                    About = $"Just another romantic movie",
                    Category = $"Romantic"
                },
                new Movie()
                {
                    Id = new Guid("A15E21E9-6430-4AC3-6FC5-08D5C3128E9D"),
                    Title = $"Sharknado",
                    YearOfRelease = 2001,
                    RunningTime = 130,
                    About = $"Movie about sharks and tornados",
                    Category = $"Scifi"
                },

                new Movie()
                {
                    Id = new Guid("5C2EBFE3-6BDE-4025-6FC6-08D5C3128E9D"),
                    Title = $"Star Wars",
                    YearOfRelease = 1980,
                    RunningTime = 122,
                    About = $"Spaceships, jedis and the dark lord",
                    Category = $"Scifi"
                },

                new Movie()
                {
                    Id = new Guid("59A12EA1-EA8F-4F5F-6FC7-08D5C3128E9D"),
                    Title = $"Die Hard 2",
                    YearOfRelease = 2003,
                    RunningTime = 111,
                    About = $"Just another action movie 2",
                    Category = $"Action"
                },
                new Movie()
                {
                    Id = new Guid("0A7B5831-6D55-49DD-6FC8-08D5C3128E9D"),
                    Title = $"Die Hard 3",
                    YearOfRelease = 2010,
                    RunningTime = 114,
                    About = $"Just another action movie 3",
                    Category = $"Action"
                },

                new Movie()
                {
                    Id = new Guid("CB436A25-3EBD-4E29-6FC9-08D5C3128E9D"),
                    Title = $"Live Easy 2",
                    YearOfRelease = 2012,
                    RunningTime = 123,
                    About = $"Just another romantic movie 2",
                    Category = $"Romantic"
                },
                new Movie()
                {
                    Id = new Guid("AD1E5CD8-F4F4-48CB-6FCA-08D5C3128E9D"),
                    Title = $"Love all around",
                    YearOfRelease = 1999,
                    RunningTime = 89,
                    About = $"Love, love, love",
                    Category = $"Romantic"
                },

                new Movie()
                {
                    Id = new Guid("0F1DB7AF-7C87-4C03-6FCB-08D5C3128E9D"),
                    Title = $"Xtro",
                    YearOfRelease = 1976,
                    RunningTime = 101,
                    About = $"Ancient terrible movie",
                    Category = $"Scifi"
                },

                new Movie()
                {
                    Id = new Guid("E84F12A0-4A20-4300-6FCC-08D5C3128E9D"),
                    Title = $"The drunken sailor",
                    YearOfRelease = 2013,
                    RunningTime = 92,
                    About = $"Some random comedy",
                    Category = $"Comedy"
                },
                new Movie()
                {
                    Id = new Guid("2F8CF945-46BA-4F4E-6FCD-08D5C3128E9D"),
                    Title = $"The drunken sailor 2",
                    YearOfRelease = 2014,
                    RunningTime = 92,
                    About = $"Some random comedy 2",
                    Category = $"Comedy"
                }
            }.AsQueryable();

            // Adding users
            var users = new List<User>()
            {
                new User()
                {
                    Id = 0,
                    Name = $"Ben Big"
                },
                new User()
                {
                    Id = 1,
                    Name = $"Mark Small"
                },
                new User()
                {
                    Id = 2,
                    Name = $"John Red"
                },
                new User()
                {
                    Id = 3,
                    Name = $"Matthew Fat"
                },
                new User()
                {
                    Id = 4,
                    Name = $"Rob Muller"
                },
                new User()
                {
                    Id = 5,
                    Name = $"Ingela Nice"
                },
                new User()
                {
                    Id = 6,
                    Name = $"Vladyslav North"
                },
                new User()
                {
                    Id = 7,
                    Name = $"Tejus Warm"
                },
                new User()
                {
                    Id = 8,
                    Name = $"Martin Good"
                },
                new User()
                {
                    Id = 9,
                    Name = $"Mirealls Green"
                }
            }.AsQueryable();

            var ratings = new List<Rating>()
            {
                new Rating()
                {
                    Id = new Guid("6E9D9210-26A4-4F4F-0329-08D5C3128EA2"),MovieId = new Guid("D3CE3D3B-D3D7-409B-6FC3-08D5C3128E9D"),RatedOn = new DateTime(2018,05,26),UserId = 0,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("071CBB1E-B02E-49A0-032A-08D5C3128EA2"),MovieId = new Guid("D3CE3D3B-D3D7-409B-6FC3-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 1,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("F7FB0EEA-86F9-4859-032B-08D5C3128EA2"),MovieId = new Guid("D3CE3D3B-D3D7-409B-6FC3-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 2,UserRating = 1
                },
                new Rating()
                {
                    Id = new Guid("CC4F9D99-F5DA-41E0-032C-08D5C3128EA2"),MovieId = new Guid("03EDB3C7-A2AE-4B27-6FC4-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 0,UserRating = 5
                },
                new Rating()
                {
                    Id = new Guid("179C7EAB-90FB-43D3-032D-08D5C3128EA2"),MovieId = new Guid("03EDB3C7-A2AE-4B27-6FC4-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 1,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("2D7EEC0C-06B2-4A6F-032E-08D5C3128EA2"),MovieId = new Guid("03EDB3C7-A2AE-4B27-6FC4-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 2,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("3FCC3EAE-4881-4542-032F-08D5C3128EA2"),MovieId = new Guid("A15E21E9-6430-4AC3-6FC5-08D5C3128E9D"),RatedOn = new DateTime(2018,03,05),UserId = 0,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("7E9F2D14-ECD0-429F-0330-08D5C3128EA2"),MovieId = new Guid("A15E21E9-6430-4AC3-6FC5-08D5C3128E9D"),RatedOn = new DateTime(2018,03,05),UserId = 1,UserRating = 1
                },
                new Rating()
                {
                    Id = new Guid("46713012-54BB-40EC-0331-08D5C3128EA2"),MovieId = new Guid("5C2EBFE3-6BDE-4025-6FC6-08D5C3128E9D"),RatedOn = new DateTime(2018,03,05),UserId = 0,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("69691EC5-5E4E-4675-0332-08D5C3128EA2"),MovieId = new Guid("5C2EBFE3-6BDE-4025-6FC6-08D5C3128E9D"),RatedOn = new DateTime(2018,03,05),UserId = 1,UserRating = 5
                },
                new Rating()
                {
                    Id = new Guid("A9433103-9925-49A1-0333-08D5C3128EA2"),MovieId = new Guid("5C2EBFE3-6BDE-4025-6FC6-08D5C3128E9D"),RatedOn = new DateTime(2018,06,05),UserId = 2,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("89F95EDA-FB35-42C7-0334-08D5C3128EA2"),MovieId = new Guid("5C2EBFE3-6BDE-4025-6FC6-08D5C3128E9D"),RatedOn = new DateTime(2018,03,04),UserId = 3,UserRating = 5
                },
                new Rating()
                {
                    Id = new Guid("D3BCD0CB-1B7A-40B7-0335-08D5C3128EA2"),MovieId = new Guid("59A12EA1-EA8F-4F5F-6FC7-08D5C3128E9D"),RatedOn = new DateTime(2018,03,05),UserId = 0,UserRating = 3
                },
                new Rating()
                {
                    Id = new Guid("CC30DF97-8310-4C02-0336-08D5C3128EA2"),MovieId = new Guid("59A12EA1-EA8F-4F5F-6FC7-08D5C3128E9D"),RatedOn = new DateTime(2018,03,05),UserId = 1,UserRating = 5
                },
                new Rating()
                {
                    Id = new Guid("E588B387-0326-4800-0337-08D5C3128EA2"),MovieId = new Guid("59A12EA1-EA8F-4F5F-6FC7-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 2,UserRating = 5
                },
                new Rating()
                {
                    Id = new Guid("1FDF6A58-5B3F-4316-0338-08D5C3128EA2"),MovieId = new Guid("0A7B5831-6D55-49DD-6FC8-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 0,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("896C2584-7330-4DF2-0339-08D5C3128EA2"),MovieId = new Guid("0A7B5831-6D55-49DD-6FC8-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 1,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("3B9A65CC-6CBD-4783-033A-08D5C3128EA2"),MovieId = new Guid("0A7B5831-6D55-49DD-6FC8-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 2,UserRating = 1
                },
                new Rating()
                {
                    Id = new Guid("73C26916-F5C6-4F2C-033B-08D5C3128EA2"),MovieId = new Guid("CB436A25-3EBD-4E29-6FC9-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 0,UserRating = 5
                },
                new Rating()
                {
                    Id = new Guid("BB42789A-4FF4-4373-033C-08D5C3128EA2"),MovieId = new Guid("CB436A25-3EBD-4E29-6FC9-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 1,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("26A8FEE4-F8F8-4031-033D-08D5C3128EA2"),MovieId = new Guid("CB436A25-3EBD-4E29-6FC9-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 2,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("73F45189-1A36-427D-033E-08D5C3128EA2"),MovieId = new Guid("AD1E5CD8-F4F4-48CB-6FCA-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 0,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("FC805DDD-4BDD-46D1-033F-08D5C3128EA2"),MovieId = new Guid("AD1E5CD8-F4F4-48CB-6FCA-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 1,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("3D7C1049-3458-427D-0340-08D5C3128EA2"),MovieId = new Guid("AD1E5CD8-F4F4-48CB-6FCA-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 4,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("C6416807-FFA6-4C00-0341-08D5C3128EA2"),MovieId = new Guid("AD1E5CD8-F4F4-48CB-6FCA-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 7,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("2A467D18-19B5-4BB8-0342-08D5C3128EA2"),MovieId = new Guid("0F1DB7AF-7C87-4C03-6FCB-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 0,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("C13AF258-E2B2-47C2-0343-08D5C3128EA2"),MovieId = new Guid("0F1DB7AF-7C87-4C03-6FCB-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 7,UserRating = 1
                },
                new Rating()
                {
                    Id = new Guid("D368F0D1-72AA-42C8-0344-08D5C3128EA2"),MovieId = new Guid("0F1DB7AF-7C87-4C03-6FCB-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 8,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid("63FE8C8C-974A-4AC1-0345-08D5C3128EA2"),MovieId = new Guid("0F1DB7AF-7C87-4C03-6FCB-08D5C3128E9D"),RatedOn = new DateTime(2018,08,04),UserId = 4,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("F06B5D91-E4CB-4A60-0346-08D5C3128EA2"),MovieId = new Guid("E84F12A0-4A20-4300-6FCC-08D5C3128E9D"),RatedOn = new DateTime(2018,08,05),UserId = 1,UserRating = 5
                },
                new Rating()
                {
                    Id = new Guid("A37C9B93-E16B-47C5-0347-08D5C3128EA2"),MovieId = new Guid("E84F12A0-4A20-4300-6FCC-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 2,UserRating = 5
                },
                new Rating()
                {
                    Id = new Guid("62C6B0B1-F5C2-42AF-0348-08D5C3128EA2"),MovieId = new Guid("E84F12A0-4A20-4300-6FCC-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 5,UserRating = 3
                },
                new Rating()
                {
                    Id = new Guid("9F9BCA27-3C63-4C68-0349-08D5C3128EA2"),MovieId = new Guid("E84F12A0-4A20-4300-6FCC-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 7,UserRating = 3
                },
                new Rating()
                {
                    Id = new Guid("ABD12385-4ECB-4309-034A-08D5C3128EA2"),MovieId = new Guid("2F8CF945-46BA-4F4E-6FCD-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 1,UserRating = 3
                },
                new Rating()
                {
                    Id = new Guid("6695E642-0ACF-41C0-034B-08D5C3128EA2"),MovieId = new Guid("2F8CF945-46BA-4F4E-6FCD-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 2,UserRating = 4
                },
                new Rating()
                {
                    Id = new Guid("432F9331-A07D-4175-034C-08D5C3128EA2"),MovieId = new Guid("2F8CF945-46BA-4F4E-6FCD-08D5C3128E9D"),RatedOn = new DateTime(2018,05,05),UserId = 5,UserRating = 2
                },
                new Rating()
                {
                    Id = new Guid( "2C31DF87-793E-4459-034D-08D5C3128EA2"),MovieId = new Guid("2F8CF945-46BA-4F4E-6FCD-08D5C3128E9D"),RatedOn = new DateTime(2018,07,05),UserId = 7,UserRating = 2
                }
            }.AsQueryable();


            repo.Setup(s => s.GetMovies()).Returns(movies);
            repo.Setup(s => s.GetRatings()).Returns(ratings);
            return repo;
        }
        public Mock<DbSet<Movie>> mockSet { get; private set; }
        public Mock<MoviesContext> mockContext { get; private set; }
        public Mock<IMoviesRepository> mockRepository { get; private set; }
        public MoviesController moviesController { get; private set; }
        public Mock<IMapper> mockMapper { get; private set; }
    }
}

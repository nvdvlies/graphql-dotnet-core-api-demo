using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGraphQL.Events;
using DemoGraphQL.Models;

namespace DemoGraphQL.Stores
{
    public class MovieStore : IMovieStore
    {
        private readonly IList<Movie> _movies;
        private readonly IEventStream<MovieCreatedEvent> _eventStream;

        public MovieStore(IEventStream<MovieCreatedEvent> eventStream)
        {
            _movies = new List<Movie>
            {
                new Movie()
                {
                    Id = 1,
                    Title = "Inception",
                    DirectorId = 1,
                    ReleaseDate = new DateTime(2014, 12, 31),
                    Length = 120
                },
                new Movie()
                {
                    Id = 2,
                    Title = "Interstellar",
                    DirectorId = 1,
                    ReleaseDate = new DateTime(2014, 12, 31),
                    Length = 120
                },
                new Movie()
                {
                    Id = 3,
                    Title = "The Dark Knight",
                    DirectorId = 1,
                    ReleaseDate = new DateTime(2008, 12, 31),
                    Length = 120
                },
                new Movie()
                {
                    Id = 4,
                    Title = "Schindler's List",
                    DirectorId = 2,
                    ReleaseDate = new DateTime(1993, 12, 31),
                    Length = 120
                },
                new Movie()
                {
                    Id = 5,
                    Title = "Jurassic Park",
                    DirectorId = 2,
                    ReleaseDate = new DateTime(1993, 12, 31),
                    Length = 120
                },
                new Movie()
                {
                    Id = 6,
                    Title = "Fight Club",
                    DirectorId = 3,
                    ReleaseDate = new DateTime(1999, 12, 31),
                    Length = 120
                },
                new Movie()
                {
                    Id = 7,
                    Title = "Se7en",
                    DirectorId = 3,
                    ReleaseDate = new DateTime(1995, 12, 31),
                    Length = 120
                }
            };

            _eventStream = eventStream;
        }

        public Task<Movie> GetMovieByIdAsync(int id)
        {
            return Task.FromResult(_movies.FirstOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<Movie>> GetMoviesByIdAsync(IEnumerable<int> ids)
        {
            return Task.FromResult(_movies.Where(x => ids.Contains(x.Id)));
        }

        public Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return Task.FromResult(_movies.AsEnumerable());
        }

        public Task<Movie> CreateAsync(Movie movie)
        {
            movie.Id = _movies.Max(x => x.Id) + 1;
            _movies.Add(movie);

            _eventStream.AddEvent(new MovieCreatedEvent(movie.Id, movie.Title));

            return Task.FromResult(movie);
        }
    }

    public interface IMovieStore
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<IEnumerable<Movie>> GetMoviesByIdAsync(IEnumerable<int> ids);
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<Movie> CreateAsync(Movie movie);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGraphQL.Models;

namespace DemoGraphQL.Stores
{
    public class DirectorStore: IDirectorStore
    {
        private readonly List<Director> _directors;

        public DirectorStore()
        {
            _directors = new List<Director>
            {
                new Director() {Id = 1, Name = "Christopher Nolan", BirthDate = new DateTime(1970, 12, 31)},
                new Director() {Id = 2, Name = "Steven Spielberg", BirthDate = new DateTime(1946, 12, 31)},
                new Director() {Id = 3, Name = "David Fincher", BirthDate = new DateTime(1962, 12, 31)}
            };
        }

        public Task<Director> GetDirectorByIdAsync(int id)
        {
            return Task.FromResult(_directors.FirstOrDefault(x => x.Id == id));
        }

        public Task<IDictionary<int, Director>> GetDirectorsByIdAsync(IEnumerable<int> ids)
        {
            return Task.FromResult(
                _directors
                    .Where(x => ids.Contains(x.Id))
                    .ToDictionary(x => x.Id) as IDictionary<int, Director>
                );
        }

        public Task<IEnumerable<Director>> GetDirectorsAsync()
        {
            return Task.FromResult(_directors.AsEnumerable());
        }
    }

    public interface IDirectorStore
    {
        Task<Director> GetDirectorByIdAsync(int id);
        Task<IDictionary<int, Director>> GetDirectorsByIdAsync(IEnumerable<int> ids);
        Task<IEnumerable<Director>> GetDirectorsAsync();
    }
}

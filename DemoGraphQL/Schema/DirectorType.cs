using DemoGraphQL.Models;
using DemoGraphQL.Stores;
using GraphQL.DataLoader;
using GraphQL.Types;
using System.Collections.Generic;

namespace DemoGraphQL.Schema
{
    public class DirectorType : ObjectGraphType<Director>
    {
        public DirectorType(IMovieStore movieStore, IDataLoaderContextAccessor accessor)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.BirthDate);

            Field<ListGraphType<MovieType>>()
                .Name("movies")
                .ResolveAsync(async ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<int, IEnumerable<Movie>>(
                        "GetMoviesDirectedByAsync",
                        movieStore.GetMoviesDirectedByAsync
                        );

                    return await loader.LoadAsync(ctx.Source.Id);
                });
        }
    }
}

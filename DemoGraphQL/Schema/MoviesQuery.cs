using DemoGraphQL.Stores;
using GraphQL.Types;

namespace DemoGraphQL.Schema
{
    public class MoviesQuery : ObjectGraphType
    {
        public MoviesQuery(IMovieStore movieStore)
        {
            Name = "Query";

            Field<ListGraphType<MovieType>>()
                .Name("movies")
                .ResolveAsync(async ctx =>
                {
                    return await movieStore.GetMoviesAsync();
                });

            Field<MovieType>()
                .Name("movie")
                .Argument<IntGraphType, int>("id", null)
                .ResolveAsync(async ctx =>
                {
                    return await movieStore.GetMovieByIdAsync(ctx.GetArgument<int>("id"));
                });
        }
    }
}

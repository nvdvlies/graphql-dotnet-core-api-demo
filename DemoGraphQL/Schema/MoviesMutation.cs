using DemoGraphQL.Models;
using DemoGraphQL.Events;
using DemoGraphQL.Stores;
using GraphQL.Types;

namespace DemoGraphQL.Schema
{
    public class MoviesMutation : ObjectGraphType
    {
        public MoviesMutation(IMovieStore moviesStore)
        {
            Name = "Mutation";

            Field<MovieType, Movie>()
                .Name("createMovie")
                .Argument<NonNullGraphType<MovieCreateInputType>>("movie", null)
                .ResolveAsync(async ctx =>
                {
                    var movieInput = ctx.GetArgument<Movie>("movie");

                    return await moviesStore.CreateAsync(movieInput);
                });
        }
    }
}

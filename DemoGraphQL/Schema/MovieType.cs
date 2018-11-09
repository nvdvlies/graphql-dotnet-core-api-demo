using DemoGraphQL.Models;
using DemoGraphQL.Stores;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace DemoGraphQL.Schema
{
    public class MovieType: ObjectGraphType<Movie>
    {
        public MovieType(IDirectorStore directorStore, IDataLoaderContextAccessor accessor)
        {
            Field(x => x.Id);
            Field(x => x.Title);
            Field(x => x.Length);
            Field(x => x.ReleaseDate);

            Field<DirectorType, Director>()
                .Name("director")
                .ResolveAsync(async ctx =>
                {
                    //N+1 problem:
                    //return await directorService.GetDirectorByIdAsync(ctx.Source.DirectorId);

                    var loader = accessor.Context.GetOrAddBatchLoader<int, Director>(
                        "GetDirectorsByIdAsync",
                        directorStore.GetDirectorsByIdAsync
                        );

                    return await loader.LoadAsync(ctx.Source.DirectorId);
                });
        }
    }
}

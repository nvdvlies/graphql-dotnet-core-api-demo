using GraphQL;

namespace DemoGraphQL.Schema
{
    public class MoviesSchema : GraphQL.Types.Schema
    {
        public MoviesSchema(
            MoviesQuery query,
            MoviesMutation mutation,
            MoviesSubscription subscription,
            IDependencyResolver resolver) : base(resolver)
        {
            Query = query;
            Mutation = mutation;
            Subscription = subscription;
        }
    }
}

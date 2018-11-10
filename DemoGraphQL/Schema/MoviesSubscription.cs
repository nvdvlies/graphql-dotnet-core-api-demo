using DemoGraphQL.Events;
using DemoGraphQL.Models;
using GraphQL.Resolvers;
using GraphQL.Types;
using System.Reactive.Linq;

namespace DemoGraphQL.Schema
{
    public class MoviesSubscription : ObjectGraphType
    {
        public MoviesSubscription(IEventStream<Movie> eventStream)
        {
            Name = "Subscription";

            AddField(new EventStreamFieldType()
            {
                Name = "movieCreated",
                Arguments = new QueryArguments(),
                Type = typeof(MovieType),
                Resolver = new FuncFieldResolver<Movie>(ctx => ctx.Source as Movie),
                Subscriber = new EventStreamResolver<Movie>(ctx => eventStream.AsObservable())
            });
        }
    }
}

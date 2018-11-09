using DemoGraphQL.Events;
using GraphQL.Resolvers;
using GraphQL.Types;
using System.Reactive.Linq;

namespace DemoGraphQL.Schema
{
    public class MoviesSubscription : ObjectGraphType
    {
        public MoviesSubscription(IEventStream<MovieCreatedEvent> eventStream)
        {
            Name = "Subscription";

            AddField(new EventStreamFieldType()
            {
                Name = "movieCreated",
                Arguments = new QueryArguments(),
                Type = typeof(MovieCreatedEventType),
                Resolver = new FuncFieldResolver<MovieCreatedEvent>(ctx => ctx.Source as MovieCreatedEvent),
                Subscriber = new EventStreamResolver<MovieCreatedEvent>(ctx => eventStream.AsObservable())
            });
        }
    }
}

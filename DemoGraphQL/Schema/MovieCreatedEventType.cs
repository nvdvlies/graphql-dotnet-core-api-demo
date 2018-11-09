using DemoGraphQL.Events;
using GraphQL.Types;

namespace DemoGraphQL.Schema
{
    public class MovieCreatedEventType : ObjectGraphType<MovieCreatedEvent>
    {
        public MovieCreatedEventType()
        {
            Field(x => x.Id);
            Field(x => x.Title);
        }
    }
}

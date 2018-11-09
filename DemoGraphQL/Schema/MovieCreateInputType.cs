using GraphQL.Types;

namespace DemoGraphQL.Schema
{
    public class MovieCreateInputType : InputObjectGraphType
    {
        public MovieCreateInputType()
        {
            Name = "MovieCreateInput";

            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<IntGraphType>>("directorId");
            Field<DateGraphType>("releaseDate");
            Field<IntGraphType>("length");
        }
    }
}

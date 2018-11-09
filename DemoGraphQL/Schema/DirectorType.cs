using DemoGraphQL.Models;
using GraphQL.Types;

namespace DemoGraphQL.Schema
{
    public class DirectorType : ObjectGraphType<Director>
    {
        public DirectorType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.BirthDate);
        }
    }
}

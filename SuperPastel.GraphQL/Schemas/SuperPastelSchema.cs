using SuperPastel.GraphQL.Mutations;
using SuperPastel.GraphQL.Queries;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace SuperPastel.GraphQL.Schemas
{
    public class SuperPastelSchema : Schema
    {
        public SuperPastelSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<Query>();
            Mutation = provider.GetRequiredService<Mutation>();
        }
    }
}

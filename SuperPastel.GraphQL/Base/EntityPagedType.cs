using SuperPastel.Nucleo.Base;
using GraphQL.Types;

namespace SuperPastel.GraphQL.Base
{
    public class EntityPagedType<TEntity, TGraph> : ObjectGraphType<PageInfo<TEntity>>
        where TEntity : Entidade<TEntity>
        where TGraph : class, IGraphType
    {
        public EntityPagedType()
        {
            Name = $"{typeof(TGraph).Name.Replace("Type", string.Empty)}PagedType";

            Field<ListGraphType<TGraph>>(
                "items",
                resolve: context => context.Source.List
            );
            Field(x => x.PageCount);
            Field(x => x.Size);
            Field(x => x.TotalCount);
        }
    }
}

using SuperPastel.Dominio.Enums;
using GraphQL.Types;

namespace SuperPastel.GraphQL.EnumTypes
{
    public class EstadoType : EnumerationGraphType<Estado>, IGraphType
    {
        public EstadoType()
        {
            Name = nameof(EstadoType);
        }
    }
}

using SuperPastel.Nucleo.Base;
using FluentValidation;
using GraphQL.Types;

namespace SuperPastel.Nucleo.Interfaces
{
    public interface IMutation<TEntidade, TEntidadeType, TEntidadeInputType, TValidacao> : IDisposable
        where TEntidade : Entidade<TEntidade>
        where TEntidadeType : ObjectGraphType<TEntidade>, IGraphType
        where TEntidadeInputType : InputObjectGraphType<TEntidade>, IGraphType
        where TValidacao : AbstractValidator<TEntidade>, new() { }
}

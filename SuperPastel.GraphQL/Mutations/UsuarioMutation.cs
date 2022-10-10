using SuperPastel.Dominio.Entidades.Usuarios;
using GraphQL.Types;
using SuperPastel.Dominio.Entidades.Usuarios.Repositorio;
using SuperPastel.GraphQL.Types.Usuarios;
using GraphQL;
using SuperPastel.GraphQL.Models;
using SuperPastel.Dominio.Entidades.Pessoas;
using SuperPastel.Nucleo.Mensageria;
using MediatR;
using SuperPastel.Nucleo.Notificacoes;
using SuperPastel.GraphQL.Base;

namespace SuperPastel.GraphQL.Mutations
{
    public class UsuarioMutation : ObjectGraphType
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioMutation(
            IMediatorHandler bus,
            INotificationHandler<NotificacaoDominio> notificacao,
            IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;

            Name = nameof(UsuarioMutation);
            Description = "Mutations de usuário";

            Field<UsuarioType>(
                "create",
                description: "Criar um novo usuário",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UsuarioInputType>> { Name = "input" }),
                resolve: context =>
                {
                    var input = context.GetArgument<UsuarioInput>("input");

                    if (EmailJaCadastrado(input.Email))
                    {
                        context.Errors.Add(new ExecutionError("E-mail já cadastrado"));
                        return null;
                    }

                    var pessoa = new Pessoa(
                        input.Pessoa.Nome,
                        input.Pessoa.DataNascimento,
                        input.Pessoa.Cep,
                        input.Pessoa.Logradouro,
                        input.Pessoa.Numero,
                        input.Pessoa.Complemento,
                        input.Pessoa.Bairro,
                        input.Pessoa.Cidade,
                        input.Pessoa.Telefone,
                        input.Pessoa.Celular,
                        input.Pessoa.FotoUrl);

                    var usuario = new Usuario(bus)
                        .Criar(input.Email, input.Senha, pessoa)
                        .ConfirmarEmail();

                    if (input.SuperUsuario)
                    {
                        usuario.EhSuperUsuario();
                    }

                    var notificacoes = (NotificacaoDominioHandler)notificacao;

                    if (notificacoes.HasNotifications())
                    {
                        context.AddErrorMessages(notificacoes);
                        return null;
                    }

                    return usuarioRepositorio.Adicionar(usuario);
                });
        }

        private bool EmailJaCadastrado(string email) 
            => _usuarioRepositorio.ObterTodos(x => x.Email == email).Any();

    }
}

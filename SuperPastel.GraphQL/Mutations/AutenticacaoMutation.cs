using SuperPastel.Dominio.Entidades.Autenticacoes;
using SuperPastel.Dominio.Entidades.Usuarios.Repositorio;
using SuperPastel.GraphQL.Types.Autenticacoes;
using SuperPastel.Nucleo.Ajudantes;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.Configuration;

namespace SuperPastel.GraphQL.Mutations
{
    public class AutenticacaoMutation : ObjectGraphType
    {
        public AutenticacaoMutation(
            IUsuarioRepositorio usuarioRepositorio,
            IConfiguration configuration)
        {
            Name = nameof(AutenticacaoMutation);

            Field<AutenticacaoType>(
                "login",
                description: "Autenticar um usuário",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "input" }
                ),
                resolve: context =>
                {
                    var login = context.GetArgument<Login>("input");
                    var usuario = usuarioRepositorio.ObterPorEmail(login.Email);

                    if (usuario == null)
                    {
                        context.Errors.Add(new ExecutionError("E-mail e/ou senha inválidos"));
                        return null;
                    }

                    if (!Passwords.VerifyHashedPassword(usuario.SenhaHash, login.Senha))
                    {
                        context.Errors.Add(new ExecutionError("E-mail e/ou senha inválidos"));
                        return null;
                    }

                    if (!usuario.EmailConfirmado)
                    {
                        context.Errors.Add(new ExecutionError("E-mail não confirmado"));
                        return null;
                    }

                    var dataValidade = DateTime.Now.AddDays(1);
                    var perfil = usuario.SuperUsuario ? "Gerente" : "Usuario";

                    var token = new JwtConfiguracao().GerarToken(
                        configuration, usuario.Id, dataValidade,
                        usuario.SuperUsuario, new string[] { perfil });

                    return new Autenticacao(perfil, token, dataValidade);

                });
        }
    }
}

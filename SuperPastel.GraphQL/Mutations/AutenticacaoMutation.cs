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
                "user",
                description: "Autenticar um usuário",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "login" }
                ),
                resolve: context =>
                {
                    var login = context.GetArgument<Login>("login");
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
                    var papel = usuario.SuperUsuario ? "Gerente" : "Usuario";

                    var token = new JwtConfiguracao().GerarToken(
                        configuration, usuario.Id, dataValidade,
                        usuario.SuperUsuario, new string[] { papel });

                    return new Autenticacao(token, dataValidade);

                });
        }
    }
}

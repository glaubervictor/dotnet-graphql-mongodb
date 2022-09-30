using SuperPastel.Infra.Dados.Repositorios;
using SuperPastel.Infra.Transporte;
using SuperPastel.Nucleo.Interfaces;
using SuperPastel.Nucleo.Mensageria;
using SuperPastel.Nucleo.Notificacoes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using SuperPastel.GraphQL.Mutations;
using SuperPastel.GraphQL.Sessao;
using SuperPastel.Infra.Dados.Contextos;

namespace SuperPastel.Infra.InversaoDeControle
{
    public class Bootstrapper
    {
        public static void RegistrarServicos(IServiceCollection services)
        {
            // Asp.Net Core
            services.AddHttpContextAccessor();

            //Contexto
            services.AddScoped<MongoDbContexto>();

            // Event Bus
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            //Sessão
            services.AddScoped<ISessaoDoUsuario, SessaoDoUsuario>();

            //Email
            //services.AddTransient<IEnvioDeEmail, EnvioDeEmail>();

            // Repositórios
            services.Scan(scan =>
                  scan.FromAssembliesOf(typeof(Repositorio<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepositorio<>)))
                    .AsImplementedInterfaces()
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelf()
                    .WithScopedLifetime());

            // Eventos
            services.AddScoped<INotificationHandler<NotificacaoDominio>, NotificacaoDominioHandler>();
        }
    }
}

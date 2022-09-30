using SuperPastel.GraphQL.Schemas;
using SuperPastel.Infra.InversaoDeControle;
using SuperPastel.Nucleo.Ajudantes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using GraphQL.Server;
using GraphQL;
using GraphQLBuilderExtensions = GraphQL.MicrosoftDI.GraphQLBuilderExtensions;

namespace SuperPastel.Api
{
    public class Startup
    {
        readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region [Boostrapper]

            Bootstrapper.RegistrarServicos(services);

            #endregion

            #region [Authentication]

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"])),

                    RequireExpirationTime = true,
                    ValidateLifetime = false,

                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion

            #region [GraphQL]

            GraphQLBuilderExtensions.AddGraphQL(services)
                .AddServer(true)
                .ConfigureExecution(options =>
                {
                    options.EnableMetrics = _env.IsDevelopment();
                    var logger = options.RequestServices.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                })
                .AddSystemTextJson()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = _env.IsDevelopment())
                .AddSchema<SuperPastelSchema>()
                .AddGraphTypes(typeof(SuperPastelSchema).Assembly)
                .AddGraphQLAuthorization(options =>
                {
                    options.AddPolicy(Policies.LOGGED, p => p.RequireAuthenticatedUser());
                    options.AddPolicy(Policies.USER, _ => _.RequireClaim(ClaimTypes.Role, "Usuario"));
                    options.AddPolicy(Policies.MANAGER, _ => _.RequireClaim(ClaimTypes.Role, "Gerente"));

                    options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                        .RequireAuthenticatedUser().Build());
                });

            services.AddGraphQLUpload();

            services.AddScoped<SuperPastelSchema>();

            #endregion

            #region [MediatR]

            services.AddMediatR(typeof(Startup));

            #endregion

            #region [CORS]

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            #endregion

            #region [MVC]

            services
                .AddControllers()
                .AddNewtonsoftJson()
                .AddControllersAsServices();

            services.AddMvc();

            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region [ENV]

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #endregion

            #region [Authentication]

            app.UseAuthentication();

            #endregion

            #region [Authorization]

            app.UseAuthorization();

            #endregion

            #region [Cors]

            app.UseCors("CorsPolicy");

            #endregion

            #region [GraphQL]

            app.UseWebSockets();

            app.UseGraphQLUpload<SuperPastelSchema>();

            app.UseGraphQL<SuperPastelSchema>()
                .UseGraphQLPlayground();

            #endregion

        }
    }
}

namespace AcmIdmConsumer.WebApi
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Be.Vlaanderen.Basisregisters.AcmIdm;
    using Be.Vlaanderen.Basisregisters.Api;
    using IdentityModel.AspNetCore.OAuth2Introspection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private IContainer _applicationContainer;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var oAuth2IntrospectionOptions = _configuration.GetSection(nameof(OAuth2IntrospectionOptions)).Get<OAuth2IntrospectionOptions>();
            var acmIdmPolicyOptions = _configuration.GetSection(nameof(AcmIdmPolicyOptions)).Get<AcmIdmPolicyOptions>();
            if (acmIdmPolicyOptions!.AllowedScopeValues.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(acmIdmPolicyOptions.AllowedScopeValues));
            }

            services.AddAcmIdmAuthentication(oAuth2IntrospectionOptions!);

            services
                .ConfigureDefaultForApi<Startup>(new StartupConfigureOptions
                {
                    Cors =
                    {
                        Origins = _configuration
                            .GetSection("Cors")
                            .GetChildren()
                            .Select(c => c.Value)
                            .ToArray()!
                    },
                    Swagger =
                    {
                        ApiInfo = (_, description) => new OpenApiInfo
                        {
                            Version = description.ApiVersion.ToString(),
                            Title = "Basisregisters Vlaanderen ACM/IDM Sample Web API"
                        },
                        XmlCommentPaths = new[] { typeof(Startup).GetTypeInfo().Assembly.GetName().Name }!
                    },
                    MiddlewareHooks =
                    {
                        Authorization = options =>
                        {
                            options.AddAcmIdmAuthorization(
                                PolicyNames.AcmIdmPolicy,
                                acmIdmPolicyOptions.AllowedScopeValues!);
                        }
                    }
                });

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new ApiModule(services));
            _applicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(_applicationContainer);
        }

        public void Configure(
            IServiceProvider serviceProvider,
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime appLifetime,
            ILoggerFactory loggerFactory,
            IApiVersionDescriptionProvider apiVersionProvider)
        {
            app
                .UseDefaultForApi(new StartupUseOptions
                {
                    Common =
                    {
                        ApplicationContainer = _applicationContainer,
                        ServiceProvider = serviceProvider,
                        HostingEnvironment = env,
                        ApplicationLifetime = appLifetime,
                        LoggerFactory = loggerFactory
                    },
                    Api =
                    {
                        VersionProvider = apiVersionProvider,
                        Info = groupName => $"Basisregisters Vlaanderen - ACM/IDM Sample Web API {groupName}",
                        CSharpClientOptions =
                        {
                            ClassName = "AcmIdmConsumer.WebApi",
                            Namespace = "Be.Vlaanderen.Basisregisters"
                        },
                        TypeScriptClientOptions =
                        {
                            ClassName = "AcmIdmConsumer.WebApi"
                        }
                    },
                    Server =
                    {
                        PoweredByName = "Vlaamse overheid - Basisregisters Vlaanderen",
                        ServerName = "Digitaal Vlaanderen"
                    }
                });
        }
    }
}

namespace AcmIdmConsumer.WebApi
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public sealed class ApiModule : Module
    {
        private readonly IServiceCollection _services;

        public ApiModule(IServiceCollection services)
        {
            _services = services;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ProblemDetailsHelper>()
                .AsSelf();

            // I think we should wrap this in an extension method.
            _services.AddSingleton<IAuthorizationHandler, AcmIdmAuthorizationHandler>();

            builder.Populate(_services);
        }
    }
}

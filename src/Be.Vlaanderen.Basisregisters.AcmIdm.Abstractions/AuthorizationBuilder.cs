namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions;

using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

public class AuthorizationBuilder
{
    public AuthorizationBuilder(IServiceCollection services)
        => Services = services;

    public virtual IServiceCollection Services { get; }

    public virtual AuthorizationBuilder SetDefaultPolicy(AuthorizationPolicy policy)
    {
        Services.Configure<AuthorizationOptions>(o => o.DefaultPolicy = policy);
        return this;
    }

    public virtual AuthorizationBuilder AddPolicy(string name, AuthorizationPolicy policy)
    {
        Services.Configure<AuthorizationOptions>(o => o.AddPolicy(name, policy));
        return this;
    }

    public virtual AuthorizationBuilder AddPolicy(string name, Action<AuthorizationPolicyBuilder> configurePolicy)
    {
        Services.Configure<AuthorizationOptions>(o => o.AddPolicy(name, configurePolicy));
        return this;
    }

    public virtual AuthorizationBuilder AddDefaultPolicy(string name, AuthorizationPolicy policy)
    {
        SetDefaultPolicy(policy);
        return AddPolicy(name, policy);
    }

    public virtual AuthorizationBuilder AddDefaultPolicy(string name, Action<AuthorizationPolicyBuilder> configurePolicy)
    {
        if (configurePolicy == null)
        {
            throw new ArgumentNullException(nameof(configurePolicy));
        }

        var policyBuilder = new AuthorizationPolicyBuilder();
        configurePolicy(policyBuilder);
        return AddDefaultPolicy(name, policyBuilder.Build());
    }
}

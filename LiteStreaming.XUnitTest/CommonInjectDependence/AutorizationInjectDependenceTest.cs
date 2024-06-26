﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using WebApi.CommonInjectDependence;
using Application.Authentication;
using IdentityServer4.AccessTokenValidation;
using LiteStreaming.WebApi.Options;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CommonInjectDependence;
public class AuthorizationInjectDependenceTest
{
    [Fact]
    public void AddAutoAuthenticationConfigurations_ShouldAddAuthenticationAndAuthorizationConfigurations()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
        var services = new ServiceCollection();

        // Act
        services.AddSigningConfigurations(configuration);
        services.AddAutoAuthenticationConfigurations();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        Assert.NotNull(serviceProvider.GetService<SigningConfigurations>());

        // Assert authentication configurations
        var authenticationOptions = serviceProvider.GetRequiredService<IOptions<AuthenticationOptions>>().Value;
        Assert.Equal(JwtBearerDefaults.AuthenticationScheme, authenticationOptions.DefaultAuthenticateScheme);
        Assert.Equal(JwtBearerDefaults.AuthenticationScheme, authenticationOptions.DefaultChallengeScheme);

        // Assert JWT bearer configurations
        var jwtBearerOptions = serviceProvider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>().Get(JwtBearerDefaults.AuthenticationScheme);
        var signingConfigurations = serviceProvider.GetRequiredService<SigningConfigurations>();

        Assert.NotNull(jwtBearerOptions);
        Assert.NotNull(jwtBearerOptions.TokenValidationParameters);
        Assert.Equal(signingConfigurations.Key, jwtBearerOptions.TokenValidationParameters.IssuerSigningKey);
        Assert.True(jwtBearerOptions.TokenValidationParameters.ValidateIssuerSigningKey);
        Assert.True(jwtBearerOptions.TokenValidationParameters.ValidateLifetime);
        Assert.Equal(TimeSpan.Zero, jwtBearerOptions.TokenValidationParameters.ClockSkew);
        
        // Assert authorization policy
        var authorizationPolicy = serviceProvider.GetRequiredService<IAuthorizationPolicyProvider>().GetPolicyAsync("Bearer")?.Result;
        Assert.NotNull(authorizationPolicy);
        Assert.Contains(JwtBearerDefaults.AuthenticationScheme, authorizationPolicy.AuthenticationSchemes);
    }

    [Fact]
    public void AddIdentityServerConfigurations_ShouldAddAuthenticationAndAuthorizationConfigurations()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
        var services = new ServiceCollection();

        // Act
        services.AddIdentityServerConfigurations(configuration);

        // Assert
        var serviceProvider = services.BuildServiceProvider();


        // Assert IdentityServer authentication configurations
        var identityServerOptions = serviceProvider.GetRequiredService<IOptions<IdentityServerConfigurations>>().Value;

        var identityServerAuthenticationOptions = serviceProvider.GetRequiredService<IOptionsMonitor<IdentityServerAuthenticationOptions>>().Get(IdentityServerAuthenticationDefaults.AuthenticationScheme);
        Assert.Equal(identityServerOptions.Authority, identityServerAuthenticationOptions.Authority);
        Assert.Equal(identityServerOptions.ApiName, identityServerAuthenticationOptions.ApiName);
        Assert.Equal(identityServerOptions.ApiSecret, identityServerAuthenticationOptions.ApiSecret);
        Assert.Equal(identityServerOptions.RequireHttpsMetadata, identityServerAuthenticationOptions.RequireHttpsMetadata);
        Assert.True(identityServerAuthenticationOptions.LegacyAudienceValidation);

        // Assert authorization policies
        var authorizationPolicyProvider = serviceProvider.GetRequiredService<IAuthorizationPolicyProvider>();
        var authorizationPolicy = authorizationPolicyProvider.GetPolicyAsync("Bearer")?.Result;
        Assert.NotNull(authorizationPolicy);
        Assert.Contains(IdentityServerAuthenticationDefaults.AuthenticationScheme, authorizationPolicy.AuthenticationSchemes);

        var rolePolicy = authorizationPolicyProvider.GetPolicyAsync("litestreaming-role-customer")?.Result;
        Assert.NotNull(rolePolicy);
        Assert.Contains(IdentityServerAuthenticationDefaults.AuthenticationScheme, rolePolicy.AuthenticationSchemes);
        Assert.Contains("Customer", rolePolicy.Requirements.OfType<RolesAuthorizationRequirement>().Single().AllowedRoles);
    }
}
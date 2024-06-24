using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Application.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using IdentityServer4.AccessTokenValidation;
using LiteStreaming.WebApi.Options;
using IdentityModel;

namespace WebApi.CommonInjectDependence;
public static class AutorizationInjectDependence
{
    public static void AddAutoAuthConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection("TokenConfigurations"));
        var options = services.BuildServiceProvider().GetService<IOptions<TokenOptions>>();
        if (options is null) throw new ArgumentNullException(nameof(options));
        var signingConfigurations = new SigningConfigurations(options);
        services.AddSingleton<SigningConfigurations>(signingConfigurations);
        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            var tokenConfiguration = signingConfigurations.TokenConfiguration;
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = signingConfigurations.Key,
                ValidAudience = tokenConfiguration.Audience,
                ValidIssuer = tokenConfiguration.Issuer,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​).RequireAuthenticatedUser().Build());
        });
    }

    public static IServiceCollection AddIdentityServerConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IdentityServerConfigurations>(configuration.GetSection("IdentityServerConfigurations"));

        var serviceProvider = services.BuildServiceProvider();
        var identityServerOptions = serviceProvider.GetRequiredService<IOptions<IdentityServerConfigurations>>().Value;


        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).AddIdentityServerAuthentication(options =>
        {
            options.Authority = identityServerOptions.Authority;
            options.ApiName = identityServerOptions.ApiName;
            options.ApiSecret = identityServerOptions.ApiSecret;
            options.RequireHttpsMetadata = identityServerOptions.RequireHttpsMetadata;
            options.LegacyAudienceValidation = true;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());
            options.AddPolicy("litestreaming-role-customer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme).RequireRole([ "Customer"]).Build());

        });

        services.AddControllers();
        return services;
    }
}

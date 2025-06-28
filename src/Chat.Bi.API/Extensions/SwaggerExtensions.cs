using Microsoft.OpenApi.Models;

namespace Chat.Bi.API.Extensions;

public static class SwaggerExtensions
{
    public static void AdicionarSwaggerDocV1(this IServiceCollection services)
    {
        services
        .AddSwaggerGen(c =>
        {
            c.IgnoreObsoleteActions();
            c.CustomSchemaIds(type => type.FullName);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header usando o esquema Bearer."
            });
        });
    }
}
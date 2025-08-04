using Chat.Bi.Application.Commands.Usuario.CriarContaUsuario;
using Chat.Bi.Core.Factories;
using Chat.Bi.Core.Repositories;
using Chat.Bi.Core.Resolvers;
using Chat.Bi.Core.Services;
using Chat.Bi.Infrastructure.Auth;
using Chat.Bi.Infrastructure.Configuration;
using Chat.Bi.Infrastructure.Configuration.Constantes;
using Chat.Bi.Infrastructure.IA;
using Chat.Bi.Infrastructure.IA.Factory.Interfaces;
using Chat.Bi.Infrastructure.IA.Llm.Ollama;
using Chat.Bi.Infrastructure.IA.QueryGenerator;
using Chat.Bi.Infrastructure.IA.RagContexto;
using Chat.Bi.Infrastructure.IA.Resolvers;
using Chat.Bi.Infrastructure.Logging;
using Chat.Bi.Infrastructure.Persistence;
using Chat.Bi.Infrastructure.Persistence.Repositories;
using Chat.Bi.Infrastructure.QueryExecutors;
using Chat.Bi.Infrastructure.QueryExecutors.Executors;
using Chat.Bi.SharedKernel.Configuration;
using Chat.Bi.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System.Diagnostics;

namespace Chat.Bi.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection ResolveDependencias(this IServiceCollection services, IConfiguration configuration)
    {
        ResolveConexaoBanco(services, configuration);
        Infraestrutura(services, configuration);
        SharedKernel(configuration);
        return services;
    }

    static void SharedKernel(IConfiguration configuration)
    {
        var criptografia = configuration.GetSection(nameof(CriptografiaSettings));
        var criptografiaSettings = criptografia.Get<CriptografiaSettings>();
        CriptografiaExtensions.Configure(criptografiaSettings);
    }

    static void Infraestrutura(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        services.Configure<ModelosIaSettings>(configuration.GetSection(nameof(ModelosIaSettings)));
        services.Configure<PollySettings>(configuration.GetSection(nameof(PollySettings)));
        services.AddTransient<IAuthService, AuthService>();
        services.AddScoped<IUsuarioAutenticadoService, UsuarioAutenticadoService>();
        services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CriarContaUsuarioCommand).Assembly));
        services.AddMemoryCache();
        ResolverRepository(services);
        ConfigurarModeloIa(services);
        ConfigurarExecutors(services);
    }

    static void ConfigurarExecutors(IServiceCollection services)
    {
        services.AddScoped<IQueryExecutor, SqlServerQueryExecutor>();
        services.AddScoped<IQueryExecutor, PostgreQueryExecutor>();
        services.AddScoped<IQueryExecutorFactory, QueryExecutorFactory>();
    }

    static void ConfigurarModeloIa(IServiceCollection services)
    {
        services.AddScoped<IModelosIaFactory, ModelosIaFactory>();
        services.AddScoped<IModelosIaService, OllamaService>();
        services.AddScoped<IQueryGeneratorService, QueryGeneratorService>();
        services.AddScoped<IRagContextoService, RagContextoService>();
        services.AddScoped<IModeloIaResolver, ModeloIaResolver>();
        
        //Ollama
        services.AddHttpClient<IModelosIaService,OllamaService>(ApisConfiguration.Ollama, (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<ModelosIaSettings>>().Value;
            client.BaseAddress = new Uri(options.Ollama.Endpoint);
            client.Timeout = TimeSpan.FromSeconds(30);
        })
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
        .AddPolicyHandler((sp, request) =>
        {
            var logger = sp.GetRequiredService<IAppLogger<OllamaService>>();
            var settings = sp.GetRequiredService<IOptions<PollySettings>>().Value;
            return GetRetryPolicy(ApisConfiguration.Ollama, logger, settings);
        })
        .AddPolicyHandler((sp, request) =>
        {
            var logger = sp.GetRequiredService<IAppLogger<OllamaService>>();
            var settings = sp.GetRequiredService<IOptions<PollySettings>>().Value;
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: settings.CircuitBreakerFailures,
                    durationOfBreak: TimeSpan.FromSeconds(settings.CircuitBreakerDurationSeconds),
                    onBreak: (outcome, timespan) => 
                        logger.LogError(outcome.Exception,$"[{ApisConfiguration.Ollama} CircuitBreaker] ABERTO por {timespan.TotalSeconds}s - Motivo: {outcome.Exception?.Message ?? outcome.Result.StatusCode.ToString()}"),
                    onReset: () => 
                        logger.LogInformation($"[{ApisConfiguration.Ollama} CircuitBreaker] RESETADO")
                );

        });
    }
    
    static void ResolverRepository(IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseEntityRepository<,>), typeof(BaseEntityRepository<,>));
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IChatConfigRepository, ChatConfigRepository>();
        services.AddScoped<IBaseDeDadosRepository, BaseDeDadosRepository>();
    }

    static void ResolveConexaoBanco(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ChatBiDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
    
    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(
        string api,
        IAppLogger<OllamaService> logger,
        PollySettings settings)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => (int)msg.StatusCode == 429)
            .WaitAndRetryAsync(
                retryCount: settings.RetryCount,
                sleepDurationProvider: attempt => 
                    TimeSpan.FromSeconds(Math.Pow(settings.InitialBackoffSeconds, attempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    logger.LogWarning($"[{api} Retry] Tentativa {retryAttempt} em {timespan.TotalSeconds}s - Erro: {outcome.Exception?.Message ?? outcome.Result.StatusCode.ToString()}");
                }
            );
    }
}
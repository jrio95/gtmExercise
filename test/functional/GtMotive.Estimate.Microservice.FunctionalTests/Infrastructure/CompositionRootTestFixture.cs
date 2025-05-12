using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Testcontainers.MongoDb;
using Xunit;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
{
    public sealed class CompositionRootTestFixture : IDisposable, IAsyncLifetime
    {
        private ServiceProvider _serviceProvider;
        private MongoDbContainer _mongoDbContainer;

        public CompositionRootTestFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task InitializeAsync()
        {
            _mongoDbContainer = new MongoDbBuilder()
                .WithImage("mongo:latest")
                .WithUsername("admin")
                .WithPassword("password")
                .Build();

            await _mongoDbContainer.StartAsync();

            var services = new ServiceCollection();
            ConfigureServices(services);
            services.AddSingleton<IMongoClient>(_ => new MongoClient(_mongoDbContainer.GetConnectionString()));
            services.AddLogging();
            services.AddBaseInfrastructure(true);
            services.AddSingleton<IMongoClient>(_ =>
                new MongoClient(_mongoDbContainer.GetConnectionString()));

            services.AddSingleton(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase("functionalTestdb");
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        public async Task DisposeAsync()
        {
            await _mongoDbContainer.StopAsync();
            await _mongoDbContainer.DisposeAsync();
            _serviceProvider?.Dispose();
        }

        public async Task UsingHandlerForRequest<TRequest>(Func<IRequestHandler<TRequest, Unit>, Task> handlerAction)
            where TRequest : IRequest
        {
            if (handlerAction == null)
            {
                throw new ArgumentNullException(nameof(handlerAction));
            }

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, Unit>>();

            await handlerAction.Invoke(handler);
        }

        public async Task UsingHandlerForRequestResponse<TRequest, TResponse>(Func<IRequestHandler<TRequest, TResponse>, Task> handlerAction)
            where TRequest : IRequest<TResponse>
        {
            if (handlerAction == null)
            {
                throw new ArgumentNullException(nameof(handlerAction));
            }

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();

            Debug.Assert(handler != null, "The requested handler has not been registered");

            await handlerAction.Invoke(handler);
        }

        public async Task UsingRepository<TRepository>(Func<TRepository, Task> handlerAction)
        {
            if (handlerAction == null)
            {
                throw new ArgumentNullException(nameof(handlerAction));
            }

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<TRepository>();

            Debug.Assert(handler != null, "The requested handler has not been registered");

            await handlerAction.Invoke(handler);
        }

        public void Dispose()
        {
            _serviceProvider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddApiDependencies();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddLogging();
            services.AddBaseInfrastructure(true);
        }
    }
}

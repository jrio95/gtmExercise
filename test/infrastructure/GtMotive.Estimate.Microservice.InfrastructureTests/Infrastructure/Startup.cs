using System;
using System.Threading;
using Acheve.AspNetCore.TestHost.Security;
using Acheve.TestHost;
using FluentResults;
using GtMotive.Estimate.Microservice.Api;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Features.CreateVehicle;
using GtMotive.Estimate.Microservice.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ =>
            {
                var mock = new Mock<IMediator>();
                mock.Setup(m => m.Send(It.IsAny<CreateVehicleCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(Result.Ok(new VehicleDto() { Id = Guid.NewGuid() }));

                return mock.Object;
            });

            services.AddAuthentication(TestServerDefaults.AuthenticationScheme)
                .AddTestServer();

            services.AddControllers(ApiConfiguration.ConfigureControllers)
                .WithApiControllers();

            services.AddBaseInfrastructure(true);
        }
    }
}

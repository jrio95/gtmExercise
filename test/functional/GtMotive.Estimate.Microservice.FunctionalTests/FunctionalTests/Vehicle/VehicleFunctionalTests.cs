using System;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Features.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.FunctionalTests.Vehicle
{
    /// <summary>
    /// VehicleFunctionalTests.
    /// </summary>
    [Collection(TestCollections.Functional)]
    public class VehicleFunctionalTests : FunctionalTestBase
    {
        public VehicleFunctionalTests(CompositionRootTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldCreateVehicleWhenDataIsValid()
        {
            // Arrange
            var plate = "Test1";
            var manufactureDate = DateTime.UtcNow.AddYears(-2);

            var command = new CreateVehicleCommand()
            {
                PlateNumber = plate,
                ManufactureDate = manufactureDate
            };

            // Act
            await Fixture.UsingHandlerForRequestResponse<CreateVehicleCommand, FluentResults.Result<VehicleDto>>(async handler =>
            {
                var result = await handler.Handle(command, default);

                // Assert
                result.IsSuccess.Should().BeTrue();

                var createdVehicle = result.Value;
                createdVehicle.PlateNumber.Should().Be(plate);
                createdVehicle.ManufactureDate.Date.Should().Be(manufactureDate.Date);

                await Fixture.UsingRepository<IVehicleRepository>(async repo =>
                {
                    var vehicle = await repo.GetByIdAsync(createdVehicle.Id);
                    vehicle.Should().NotBeNull();
                    vehicle.PlateNumber.Should().Be(plate);
                    vehicle.ManufactureDate.Date.Should().Be(manufactureDate.Date);
                });
            });
        }
    }
}

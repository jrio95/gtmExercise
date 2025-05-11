using System;
using FluentAssertions;
using GtMotive.Estimate.Microservice.UnitTests.Builders;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public class VehicleUnitTests
    {
        [Fact]
        public void RentShouldSucceedWhenVehicleIsAvailable()
        {
            // Arrange
            var vehicle = new VehicleBuilder().Build();
            var clientId = Guid.NewGuid();

            // Act
            var result = vehicle.Rent(clientId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            vehicle.IsRented.Should().BeTrue();
            vehicle.RentedById.Should().Be(clientId);
        }

        [Fact]
        public void RentShouldFailWhenVehicleIsAlreadyRented()
        {
            // Arrange
            var firstClient = Guid.NewGuid();
            var secondClient = Guid.NewGuid();

            var vehicle = new VehicleBuilder()
                .RentedBy(firstClient)
                .Build();

            // Act
            var result = vehicle.Rent(secondClient);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == "This vehicle can not be rented");
        }

        [Fact]
        public void RentShouldFailWhenVehicleIsDeprecated()
        {
            // Arrange
            var oldVehicle = new VehicleBuilder()
                .WithManufactureDate(DateTime.UtcNow.AddYears(-6))
                .Build();

            var clientId = Guid.NewGuid();

            // Act
            var result = oldVehicle.Rent(clientId);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == "This vehicle can not be rented");
        }

        [Fact]
        public void RentShouldFailWhenClientIdIsEmpty()
        {
            // Arrange
            var vehicle = new VehicleBuilder().Build();
            var emptyClientId = Guid.Empty;

            // Act
            var result = vehicle.Rent(emptyClientId);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == "Client does not exist");
        }
    }
}

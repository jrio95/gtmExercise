using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.InfrastructureTests
{
    [Collection(TestCollections.TestServer)]
    public class VehicleInfrastructureTests : InfrastructureTestBase
    {
        public VehicleInfrastructureTests(GenericInfrastructureTestServerFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task PostVehicleShouldReturnBadRequestWhenModelIsInvalid()
        {
            var client = Fixture.Server.CreateClient();

            var invalidPayload = new
            {
                plateNumber = string.Empty,
                manufacturedDate = "2024-01-01"
            };

            var response = await client.PostAsJsonAsync("api/vehicle", invalidPayload);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PostVehicleShouldReturnSuccessWhenModelIsValid()
        {
            var client = Fixture.Server.CreateClient();

            var validPayload = new
            {
                plateNumber = "1234ABC",
                manufacturedDate = "2024-01-01"
            };

            var response = await client.PostAsJsonAsync("api/vehicle", validPayload);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

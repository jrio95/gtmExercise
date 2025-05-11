using System;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.UnitTests.Builders
{
    public class VehicleBuilder
    {
        private string plate = "1234A";
        private DateTime manufactureDate = DateTime.UtcNow.AddYears(-2);
        private bool isRented;
        private Guid? rentedById;

        public VehicleBuilder WithPlate(string plate)
        {
            this.plate = plate;
            return this;
        }

        public VehicleBuilder WithManufactureDate(DateTime date)
        {
            manufactureDate = date;
            return this;
        }

        public VehicleBuilder RentedBy(Guid clientId)
        {
            isRented = true;
            rentedById = clientId;
            return this;
        }

        public Vehicle Build()
        {
            var vehicle = Vehicle.Create(plate, manufactureDate).Value;

            if (isRented)
            {
                vehicle.Rent(rentedById!.Value);
            }

            return vehicle;
        }
    }
}

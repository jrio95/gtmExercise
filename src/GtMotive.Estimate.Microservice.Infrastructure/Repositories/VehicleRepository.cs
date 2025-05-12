using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces.Repositories;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly MongoService _mongoService;

        public VehicleRepository(IMongoDatabase database)
        {
            _mongoService = new MongoService(database);
        }

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, id);
            return await _mongoService.Vehicles.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            try
            {
                await _mongoService.Vehicles.InsertOneAsync(vehicle);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableAsync()
        {
            var filter = Builders<Vehicle>.Filter.And(
                Builders<Vehicle>.Filter.Eq(v => v.IsRented, false),
                Builders<Vehicle>.Filter.Gt(v => v.ManufactureDate, DateTime.UtcNow.AddYears(-5)));

            return await _mongoService.Vehicles.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, vehicle.Id);
            await _mongoService.Vehicles.ReplaceOneAsync(filter, vehicle);
        }
    }
}

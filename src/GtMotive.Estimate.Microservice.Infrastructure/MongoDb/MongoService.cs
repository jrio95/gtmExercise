using GtMotive.Estimate.Microservice.Domain.Entities;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public class MongoService
    {
        private readonly IMongoDatabase _database;

        public MongoService(IMongoDatabase database)
        {
            _database = database;

            // Add call to RegisterBsonClasses() method.
        }

        public IMongoCollection<Vehicle> Vehicles =>
            _database.GetCollection<Vehicle>("vehicles");

        public IMongoCollection<Client> Clients =>
            _database.GetCollection<Client>("clients");
    }
}

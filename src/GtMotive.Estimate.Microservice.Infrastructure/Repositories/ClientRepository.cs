using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Repositories;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MongoService _mongoService;

        public ClientRepository(IMongoDatabase database)
        {
            _mongoService = new MongoService(database);
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            var filter = Builders<Client>.Filter.Eq(v => v.Id, id);
            return await _mongoService.Clients.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Client> GetByCardNumberAsync(string idCardNumber)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.IdCardNumber, idCardNumber);
            return await _mongoService.Clients.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Client client)
        {
            await _mongoService.Clients.InsertOneAsync(client);
        }

        public async Task UpdateAsync(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var filter = Builders<Client>.Filter.Eq(v => v.Id, client.Id);
            await _mongoService.Clients.ReplaceOneAsync(filter, client);
        }
    }
}

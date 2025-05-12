using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces.Repositories;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Services
{
    /// <summary>
    /// ClientService.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="clientRepository">The client repository.</param>
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Gets the or create client.
        /// </summary>
        /// <param name="clientIdCardNumber">The client identifier card number.</param>
        /// <returns>Requested Client.</returns>
        public async Task<Result<Client>> GetOrCreateClientAsync(string clientIdCardNumber)
        {
            var client = await _clientRepository.GetByCardNumberAsync(clientIdCardNumber);

            if (client == null)
            {
                var createdClientResult = await CreateNewClient(clientIdCardNumber);
                if (createdClientResult.IsFailed)
                {
                    return Result.Fail("Error while creating the client");
                }

                client = createdClientResult.Value;
            }

            return client;
        }

        /// <summary>
        /// Updates the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>Task.</returns>
        public async Task UpdateClientAsync(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        private async Task<Result<Client>> CreateNewClient(string clientIdCardNumber)
        {
            var clientToCreateResult = Client.Create(clientIdCardNumber);
            if (clientToCreateResult.IsFailed)
            {
                return clientToCreateResult;
            }

            var client = clientToCreateResult.Value;
            await _clientRepository.AddAsync(client);

            return client;
        }
    }
}

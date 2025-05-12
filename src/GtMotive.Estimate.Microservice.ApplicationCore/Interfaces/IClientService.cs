using System.Threading.Tasks;
using FluentResults;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Interfaces
{
    /// <summary>
    /// IClientService.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Gets the or create client.
        /// </summary>
        /// <param name="clientIdCardNumber">The client identifier card number.</param>
        /// <returns>Requested Client.</returns>
        Task<Result<Client>> GetOrCreateClientAsync(string clientIdCardNumber);

        /// <summary>
        /// Updates the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>Task.</returns>
        Task UpdateClientAsync(Client client);
    }
}

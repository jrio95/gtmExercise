using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Repositories
{
    /// <summary>
    /// IClientRepository.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Vehicle.</returns>
        Task<Client> GetByIdAsync(Guid id);

        /// <summary>
        /// Add a Client.
        /// </summary>
        /// <param name="client">Client to be added.</param>
        /// <returns>Task.</returns>
        Task AddAsync(Client client);

        /// <summary>
        /// Gets the by card number asynchronous.
        /// </summary>
        /// <param name="idCardNumber">The identifier card number.</param>
        /// <returns>Client.</returns>
        Task<Client> GetByCardNumberAsync(string idCardNumber);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="client">The Client.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(Client client);
    }
}

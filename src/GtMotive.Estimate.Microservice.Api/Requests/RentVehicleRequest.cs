using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Requests
{
    /// <summary>
    /// RentVehicleRequest.
    /// </summary>
    public class RentVehicleRequest
    {
        /// <summary>
        /// Gets or sets the identifier card number.
        /// </summary>
        /// <value>
        /// The identifier card number.
        /// </value>
        [Required]
        public string ClientIdCardNumber { get; set; }
    }
}

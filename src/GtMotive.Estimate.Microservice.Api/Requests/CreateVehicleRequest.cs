using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.Requests
{
    /// <summary>
    /// CreateVehicleRequest.
    /// </summary>
    public class CreateVehicleRequest
    {
        /// <summary>
        /// Gets or sets PlateNumber.
        /// </summary>
        [Required]
        public string PlateNumber { get; set; }

        /// <summary>
        /// Gets or sets manufacture date.
        /// </summary>
        [Required]
        public DateTime ManufactureDate { get; set; }
    }
}

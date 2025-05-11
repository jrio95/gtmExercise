using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Dtos
{
    /// <summary>
    /// CreateVehicleResponse.
    /// </summary>
    public class VehicleDto
    {
        /// <summary>
        /// Gets or sets PlateNumber.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets manufacture date.
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// Gets or sets the manufacture date.
        /// </summary>
        /// <value>
        /// The manufacture date.
        /// </value>
        public DateTime ManufactureDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rented.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rented; otherwise, <c>false</c>.
        /// </value>
        public bool IsRented { get; set; }
    }
}

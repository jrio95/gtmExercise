using System;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets manufacture date.
        /// </summary>
        [Required]
        public string PlateNumber { get; set; }

        /// <summary>
        /// Gets or sets the manufacture date.
        /// </summary>
        /// <value>
        /// The manufacture date.
        /// </value>
        [Required]
        public DateTime ManufactureDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rented.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rented; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool IsRented { get; set; }

        /// <summary>
        /// Gets a value the id of the client who rented the vehicle if exists.
        /// </summary>
        public Guid? RentedById { get; private set; }
    }
}

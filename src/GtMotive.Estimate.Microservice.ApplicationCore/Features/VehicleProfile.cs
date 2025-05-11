using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using DomainVehicle = GtMotive.Estimate.Microservice.Domain.Entities.Vehicle;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Features
{
    /// <summary>
    /// VehicleProfile.
    /// </summary>
    public class VehicleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleProfile"/> class.
        /// </summary>
        public VehicleProfile()
        {
            CreateMap<DomainVehicle, VehicleDto>();
        }
    }
}

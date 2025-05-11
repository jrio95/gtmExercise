using AutoMapper;
using GtMotive.Estimate.Microservice.Api.Requests;
using GtMotive.Estimate.Microservice.ApplicationCore.Commands.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Commands.RentVehicle;

namespace GtMotive.Estimate.Microservice.Api.Controllers.Vehicle
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<CreateVehicleRequest, CreateVehicleCommand>();
            CreateMap<RentVehicleRequest, RentVehicleCommand>();
            CreateMap<ReturnVehicleRequest, ReturnVehicleCommand>();
        }
    }
}

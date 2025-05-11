using AutoMapper;
using GtMotive.Estimate.Microservice.Api.Requests;
using GtMotive.Estimate.Microservice.ApplicationCore.Features.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Features.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.Features.ReturnVehicle;

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

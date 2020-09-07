using AutoMapper;
using RentACarApi.Domain;
using RentACarApi.Dtos;

namespace RentACarApi.Helpers
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<RentACarCompany, CompanyToReturn>();
            CreateMap<Vehicle, VehicleToReturn>();
            CreateMap<Vehicle, DiscountedVehicleToReturn>();
            CreateMap<Reservation, ReservationToReturn>();
        }
    }
}

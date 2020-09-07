using AutoMapper;
using AvioApi.Domain.Entities;
using AvioApi.Dtos;

namespace AvioApi.Helpers
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<AirCompany, AirCompanyToReturn>();
        }
    }
}

using AutoMapper;

namespace WineInfo.API.Profiles
{
    public class MesurementProfile : Profile 
    {
        public MesurementProfile()
        {
            CreateMap<Entities.Mesurement, Models.MesurementDto>();
            CreateMap<Entities.Variety, Models.VarietyDto>();
            CreateMap<Entities.WineType, Models.WineTypeDto>();
        }
    }
}

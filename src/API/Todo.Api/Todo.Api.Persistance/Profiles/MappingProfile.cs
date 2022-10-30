using AutoMapper;
using Todo.Api.Persistance.Models;

namespace Todo.Api.Persistance.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ListFilterDto, Application.Models.Filters.ListFilterDto>().ReverseMap();
        }

    }
}
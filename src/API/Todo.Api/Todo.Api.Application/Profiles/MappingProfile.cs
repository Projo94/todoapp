using AutoMapper;
using Todo.Api.Application.Features.Queries.GetTaskList;
using Todo.Api.Application.Features.TaskList.Queries.GetTaskList;
using Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList;
using Todo.Api.Application.Filtering;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Profiles
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.TaskList, TaskListVm>()
                .ForMember(dest => dest.DailyList, opt => opt.MapFrom(src => TimeZoneInfo.ConvertTimeBySystemTimeZoneId(src.DailyList, src.TimeZone).DateTime));

            CreateMap<ListFilter, Filtering.ListFilterDto>().ReverseMap();
            CreateMap<ListFilter, Models.Filters.ListFilterDto>().ReverseMap();

            CreateMap<TaskListDto, Entities.TaskList>().ReverseMap();

            CreateMap<CreateTaskListCommand, CreateTaskListDto>().ReverseMap();
        }
    }
}
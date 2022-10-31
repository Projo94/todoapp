using AutoMapper;
using Todo.Api.Application.Features.Queries.GetTaskList;
using Todo.Api.Application.Features.Task.Commands.CreateTask;
using Todo.Api.Application.Features.Task.Commands.UpdateTask;
using Todo.Api.Application.Features.TaskList.Queries.GetTaskList;
using Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList;
using Todo.Api.Application.Features.TaskLists.Commands.UpdateTaskList;
using Todo.Api.Application.Filtering;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Profiles
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.TaskList, TaskListVm>()
                .ForMember(dest => dest.DailyList,
                           opt => opt.MapFrom(src => TimeZoneInfo.ConvertTimeBySystemTimeZoneId(src.DailyList, src.TimeZone).DateTime));

            CreateMap<ListFilter, ListFilterDto>().ReverseMap();
            CreateMap<ListFilter, Models.Filters.ListFilterDto>().ReverseMap();

            CreateMap<TaskListDto, Entities.TaskList>().ReverseMap();

            CreateMap<CreateTaskListCommand, CreateTaskListDto>().ReverseMap();
            CreateMap<UpdateTaskListCommand, UpdateTaskListDto>().ReverseMap();

            CreateMap<Entities.TaskList, CreateTaskListCommand>().ReverseMap();
            CreateMap<Entities.TaskList, UpdateTaskListCommand>().ReverseMap();

            CreateMap<Entities.Task, CreateTaskCommand>().ReverseMap();
            CreateMap<Entities.Task, UpdateTaskCommand>().ReverseMap();

            CreateMap<Entities.Task, TaskDto>().ReverseMap();
            CreateMap<TaskFilter, Models.Filters.TaskFilterDto>().ReverseMap();

            CreateMap<Entities.Task, Features.Task.Queries.GetTask.TaskVm>().ReverseMap();
        }
    }
}
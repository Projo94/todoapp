using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Application.Features.Queries.GetTaskList;
using Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList;
using Todo.Api.Application.Features.TaskLists.Commands.DeleteTaskList;
using Todo.Api.Application.Features.TaskLists.Commands.UpdateTaskList;
using Todo.Api.Application.Features.TaskLists.Queries.GetTaskList;
using Todo.Api.Application.Filtering;

namespace Todo.Api.Controllers
{
    [EnableCors("Open")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : BaseApiController
    {
        private readonly IMapper _mapper;
        public TaskListController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("all", Name = "GetTaskListAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<TaskListVm>>> GetTaskListAsync([FromQuery] ListFilterDto filterDto)
        {
            var filter = _mapper.Map<ListFilter>(filterDto);
            filter.UserId = User.Identity?.Name ?? throw new Exception();

            var dtos = await Mediator.Send(new GetTaskListQuery { filter = filter });

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<CreateTaskListCommandResponse>> Create([FromBody] CreateTaskListDto createTaskListDto)
        {
            var createTaskList = _mapper.Map<CreateTaskListCommand>(createTaskListDto);
            createTaskList.CreatedByUserId = User.Identity?.Name ?? throw new Exception();

            var response = await Mediator.Send(createTaskList);

            return Ok(response);
        }

        [HttpDelete("{id}", Name = "DeleteTaskList")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteTaskList = new DeleteTaskListCommand { Id = id };

            await Mediator.Send(deleteTaskList);

            return NoContent();
        }

        [HttpPut(Name = "UpdateTaskList")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateTaskListDto updateTaskListDto)
        {
            var updateTaskListCommand = _mapper.Map<UpdateTaskListCommand>(updateTaskListDto);
            updateTaskListCommand.CreatedByUserId = User.Identity?.Name ?? throw new Exception();

            await Mediator.Send(updateTaskListCommand);

            return NoContent();
        }
    }
}
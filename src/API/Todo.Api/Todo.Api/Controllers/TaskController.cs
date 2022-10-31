using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Application.Features.Task.Commands.CreateTask;
using Todo.Api.Application.Features.Task.Commands.DeleteTask;
using Todo.Api.Application.Features.Task.Commands.UpdateTask;
using Todo.Api.Application.Features.Task.Queries.GetTask;
using Queries = Todo.Api.Application.Features.TaskList.Queries.GetTaskList;
using Todo.Api.Application.Filtering;

namespace Todo.Api.Controllers
{
    [EnableCors("Open")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseApiController
    {
        [HttpGet("all", Name = "GetTaskAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<Queries.TaskVm>>> GetTaskAsync([FromQuery] TaskFilter filter)
        {
            var dtos = await Mediator.Send(new GetTaskQuery { filter = filter });

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<CreateTaskCommandResponse>> CreateTask([FromBody] CreateTaskCommand createTask)
        {
            var response = await Mediator.Send(createTask);

            return Ok(response);
        }

        [HttpDelete("{id}", Name = "DeleteTask")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteTask = new DeleteTaskCommand() { Id = id };

            await Mediator.Send(deleteTask);

            return NoContent();
        }

        [HttpPut(Name = "UpdateTask")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateTaskCommand updateTaskCommand)
        {
            await Mediator.Send(updateTaskCommand);
            return NoContent();
        }
    }
}
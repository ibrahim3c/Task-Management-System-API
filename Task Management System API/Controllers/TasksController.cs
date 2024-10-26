using Core.DTOS;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Core.Constants.GeneralConsts;

namespace Task_Management_System_API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetAllTasksAsync()
        {
            var tasks = await taskService.GetAllTasksAsync();
            if (tasks.Success)
                return Ok(tasks.Data);

            return BadRequest(tasks.Messages);
        }

        [HttpGet("ProjectTasks/{id:int}")]
        public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetAllProjectTasksAsync(int id)
        {
            var tasks = await taskService.GetProjectTasksAsync(id);
            if (tasks.Success)
                return Ok(tasks.Data);

            return BadRequest(tasks.Messages);
        }

        [HttpGet("TaskStatus/{status:int}")]
        public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetAllTasksOfStatusAsync(ProjectTaskStatus status)
        {
            var tasks = await taskService.GetAllTasksOfStatus(status);
            if (tasks.Success)
                return Ok(tasks.Data);

            return BadRequest(tasks.Messages);
        }

        [HttpGet("TaskStatusByName/{status:alpha}")]
        public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetAllTasksOfStatusAsync(string status)
        {
            var tasks = await taskService.GetAllTasksOfStatus(status);
            if (tasks.Success)
                return Ok(tasks.Data);

            return BadRequest(tasks.Messages);
        }

        [HttpGet("ProjectTasksOfStatus/{id:int}/{status:int}")]
        public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetAllProjectTasksOfStatusAsync(int id,ProjectTaskStatus status)
        {
            var tasks = await taskService.GetProjectTasksOfStatusAsync(id,status);
            if (tasks.Success)
                return Ok(tasks.Data);

            return BadRequest(tasks.Messages);
        }



    }
}

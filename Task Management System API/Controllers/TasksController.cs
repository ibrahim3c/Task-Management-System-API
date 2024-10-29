using Core.DTOS;
using Core.Services.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetAllProjectTasksOfStatusAsync(int id, ProjectTaskStatus status)
        {
            var tasks = await taskService.GetProjectTasksOfStatusAsync(id, status);
            if (tasks.Success)
                return Ok(tasks.Data);

            return BadRequest(tasks.Messages);
        }
        [HttpGet("AttachmentOfTask/{id:int}")]
        public async Task<ActionResult<ResultDTO<IEnumerable<GetAttachmentDTO>>>> GetAllAttachmentsOfTaskAsync(int id){
            var attachments=await taskService.GetAllAttachmentsOfTaskAsync(id);
            if (!attachments.Success)
                return BadRequest(attachments.Messages);
            return Ok(attachments.Data);

            }

        [HttpPost]
        public async Task<ActionResult<GetTaskDTO>>AddTaskAsync(AddUpdateTaskDTO taskDTO)
        {
            if (!ModelState.IsValid)
            {
                // Collect all model state errors
                // it not important
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .SelectMany(ms => ms.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(ResultDTO<GetTaskDTO>.Failure(errors));
            }
            var result= await taskService.AddTaskAsync(taskDTO);
           if(!result.Success)
                return BadRequest(result.Messages);
           return Ok(result);
        }


        [HttpPost("test")]
        public async Task<ActionResult<GetTaskDTO>> Test(AddUpdateTaskDTOWithNoAttachment taskDTO)
        {
            if (!ModelState.IsValid)
            {               

                return BadRequest(ResultDTO<GetTaskDTO>.Failure(new List<string>{ "Failed"}));
            }
            var result = await taskService.AddTaskWithoutAttachmentAsync(taskDTO);
            if (!result.Success)
                return BadRequest(result.Messages);
            return Ok(result);
        }

        [HttpDelete("{taskID}")]
        public async Task<ActionResult<GetTaskDTO>> DeleteTaskAsync(int taskID)
        {
           var result= await taskService.DeleteTaskAsync(taskID);
            if (!result.Success)
                return BadRequest(result.Messages);

            return Ok(result);

        }

        [HttpDelete("DeleteProjectTasks/{projectID}")]
        public async Task<IActionResult> DeleteProjectTasksAsync(int projectID)
        {
            var result = await taskService.DeleteProjectTasksAsync(projectID);
            if (!result.Success)
                return BadRequest(result.Messages);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ResultDTO<GetTaskDTO>>> UpdateTaskAsync([FromForm]AddUpdateTaskDTO taskDTO)
        {
            var result = await taskService.UpdateTaskAsync(taskDTO);
            if (!result.Success)
                return BadRequest(result.Messages);

            return Ok(result);
        }

        [HttpPut("UpdateAttachmentOfTask")]
        public async Task<ActionResult<ResultDTO<GetAttachmentDTO>>> UpdateAttachmentOfTaskAsync([FromBody]AddUpdateTaskAttachmentDTO AttachmentDTO)
        {
            var result=await taskService.UpdateAttachmentToTaskAsync(AttachmentDTO);
            if (!result.Success)
                return BadRequest(result.Messages);

            return Ok(result);
        }
    }
}

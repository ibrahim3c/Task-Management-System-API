using Core.DTOS;
using Core.IRepositoreis.UOW;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Task_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<GetProjectDTO>>> GetAllProjectAsync()
        {
            return Ok(await projectService.GetAllProjectAsync());
        }

        [HttpGet("GetAllProjectAttachments/{id:int}")]
        public async Task<ActionResult<IEnumerable<GetProjectDTO>>> GetAllProjectAttachmetsAsync(int id)
        {
           var result=await projectService.GetProjectAttachmentsAsync(id);
            if(result.Success) 
                return Ok(result);
            return BadRequest(result.Data);
        }

        [HttpGet("{id:int}",Name ="GetProject")]
        public async Task<ActionResult<GetProjectDTO>> GetProjectByIdAsync(int id)
        {
            var project = await projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }
      

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResultDTO<Project>>> DeleteProjectByIdAsync(int id)
        {
            var ProjectResult=await projectService.DeleteProjectAsync(id);
            if (ProjectResult.Success)
            {
                return Ok(ProjectResult);
            }
            else
            {
                return BadRequest(ProjectResult);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultDTO<Project>>>AddProjectAsync([FromBody]AddUpdateProjectDTO projectDTO)
        {
            if(!ModelState.IsValid) 
                return BadRequest();

          var result=await projectService.AddProjectAsync(projectDTO);
          if(!result.Success) 
                return BadRequest(result);

            //return CreatedAtAction(nameof(GetProjectByIdAsync), new { id = result.Data.ProjectId }, result);
            //var url = Url.Action(nameof(GetProjectByIdAsync), new { id = result.Data.ProjectId });
            var url = "https://localhost:7191/" + result.Data.ProjectId; // this is only that works


            return Created(url, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResultDTO<Project>>> UpdateProjectAsync([FromRoute] int id, [FromBody ]AddUpdateProjectDTO projectDTO)
        {
            if (!ModelState.IsValid) 
                return BadRequest();
            var result = await projectService.UpdateProjectAsync(id, projectDTO);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
    }
}

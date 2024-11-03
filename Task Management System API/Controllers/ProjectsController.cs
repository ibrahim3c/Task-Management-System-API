using Core.Constants;
using Core.DTOS;
using Core.Services.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Task_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.AdminRole + "," + Roles.UserRole)]
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
            return BadRequest(result.Messages);
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
                return BadRequest(ProjectResult.Messages);
            }
        }



        [HttpPost]
        public async Task<ActionResult<ResultDTO<Project>>>AddProjectAsync([FromBody]AddUpdateProjectDTO projectDTO)
        {
            if(!ModelState.IsValid) 
                return BadRequest();
            // Get user ID from claims
            //projectDTO.UserId = User.Claims
            //    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var result =await projectService.AddProjectAsync(projectDTO);
          if(!result.Success) 
                return BadRequest(result.Messages);

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
                return BadRequest(result.Messages);
            }
            return Ok(result);

        }
    }
}

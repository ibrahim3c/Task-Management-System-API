using Core.Constants;
using Core.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Management_System_API.Services.Interfaces;

namespace Task_Management_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.AdminRole)]
    public class RolesController:ControllerBase
    {
        private readonly IRolesService rolesService;

        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoleAsync()
        {
            var roles=await rolesService.GetAllRolesAsync();
            if(roles.Success)
                return Ok(roles);
            return BadRequest(roles.Messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllRoleAsync(string id)
        {
            var role = await rolesService.GetRoleByIdAsync(id);
            if (role.Success)
                return Ok(role);
            return BadRequest(role.Messages);
        }


        [HttpPost()]
        public async Task<IActionResult> AddNewRoleAsync(GetRoleDTO RoleDTO)
        {
            var role = await rolesService.AddRoleAsync(RoleDTO);
            if (role.Success)
                return Ok(role);
            return BadRequest(role.Messages);
        }


        [HttpPut()]
        public async Task<IActionResult> UpdateRole(GetRoleDTO RoleDTO)
        {
            var role = await rolesService.UpdateRoleAsync(RoleDTO);
            if (role.Success)
                return Ok(role);
            return BadRequest(role.Messages);
        }





    }
}

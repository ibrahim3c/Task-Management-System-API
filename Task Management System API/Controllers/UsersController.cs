using Core.Constants;
using Core.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_Management_System_API.Services.Interfaces;

namespace Task_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.AdminRole)]
    public class UsersController:ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var users=await userService.GetAllUsersAsync();
            if(users.Success )
                return Ok(users);
            return BadRequest(users.Messages);
        }


        [HttpGet("getRolesOfUser/{userId}")]
        public async Task<IActionResult> GetUserRolesAsync(string userId)
        {
            var roles = await userService.GetRolesOfUserAsync(userId);
            if (roles.Success)
                return Ok(roles);
            return BadRequest(roles.Messages);
        }

        [HttpGet("SysUser")]
        public async Task<IActionResult> GetUserSystem()
        {
            var userID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).ToString();

            var user = await userService.GetUserByIdAsync(userID);
            if (user.Success)
                return Ok(user);
            return BadRequest(user.Messages);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            var user = await userService.GetUserByIdAsync(userId);
            if (user.Success)
                return Ok(user);
            return BadRequest(user.Messages);
        }

        [HttpGet("getByemail/{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var user = await userService.GetUserByEmailAsync(email);
            if (user.Success)
                return Ok(user);
            return BadRequest(user.Messages);
        }

        [HttpGet("GetUserProjects/{userId}")]

        public async Task<IActionResult> GetUserProjectsAsync(string userId)
        {
            var userProjects = await userService.GetAllUserProjectsAsync(userId);
            if (userProjects.Success)
                return Ok(userProjects);
            return BadRequest(userProjects.Messages);
        }


        [HttpPost()]
        public async Task<IActionResult> AddNewUserAsync([FromBody]UserRegisterDTO userRegisterDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var addResult=await userService.AddUserAsync(userRegisterDTO);
            if (addResult.Success)
                return Ok(addResult);
            return BadRequest(addResult.Messages);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addResult = await userService.UpdateUserAsync(userDTO);
            if (addResult.Success)
                return Ok(addResult);
            return BadRequest(addResult.Messages);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync( string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deleteResult = await userService.DeleteUserAsync(userId);
            if (deleteResult.Success)
                return Ok(deleteResult);
            return BadRequest(deleteResult.Messages);
        }


        [HttpDelete("DeleteByEmail/{email}")]
        public async Task<IActionResult> DeleteUserByEmailAsync(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deleteResult = await userService.DeleteUserByEmailAsync(email);
            if (deleteResult.Success)
                return Ok(deleteResult);
            return BadRequest(deleteResult.Messages);
        }

        [HttpPost("ManageUserRoles")]
        public async Task<IActionResult>ManageUserRolesAsync(ManageRolesDTO manageRolesDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await userService.ManageUserRolesAsync(manageRolesDTO);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Messages);
        }
    }
}

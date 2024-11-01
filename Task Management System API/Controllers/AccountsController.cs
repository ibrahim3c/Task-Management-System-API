using Core.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Management_System_API.Services.Interfaces;

namespace Task_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService authService;

        public AccountsController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<AuthResultDTO>>RegisterAsync(UserRegisterDTO userDTO)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            var result = await authService.RegisterAsync(userDTO);
            if (!result.Success)
                return BadRequest(result.Messages);
            return Ok(result);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResultDTO>> LoginAsync(UserLoginDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await authService.LoginAsync(userDTO);
            if (!result.Success)
                return BadRequest(result.Messages);
            return Ok(result);

        }
    }
}

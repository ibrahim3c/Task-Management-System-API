using Core.Constants;
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


        [HttpPost("RegisterWithRefreshToken")]
        public async Task<ActionResult<AuthResultDTO>> RegisterWithRefreshTokenAsync(UserRegisterDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await authService.RegisterWithRefreshTokenAsync(userDTO);
            if (!result.Success)
                return BadRequest(result.Messages);

            //set refresh token in response cookie
            setRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiresOn);
            return Ok(result);

        }


        [HttpPost("LoginWithRefreshToken")]
        public async Task<ActionResult<AuthResultDTO>> LoginWithRefreshTokenAsync(UserLoginDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await authService.LoginWithRefreshTokenAsync(userDTO);
            if (!result.Success)
                return BadRequest(result.Messages);

            //set refresh token in response cookie
            setRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiresOn);
            return Ok(result);

        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefershTokenAsync()
        {
            var refreshToken = HttpContext.Request.Cookies[GeneralConsts.RefreshTokenKey];

            var result=await authService.RefreshTokenAsync(refreshToken);
            if (!result.Success)
                return BadRequest(result.Messages);

            // set new refreshToken in response cookie
            setRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiresOn);

            return Ok(result);
        }

        [HttpGet("RevokeToken")]
        public async Task<IActionResult>RevokeTokenAsync([FromBody]string? refreshToken)
        {
            string token = refreshToken ?? HttpContext.Request.Cookies[GeneralConsts.RefreshTokenKey];
           var result=await authService.RevokeTokenAsync(token);
            if (result)
                return Ok();
            return BadRequest("InValid Token");
        }

        private void setRefreshTokenInCookie(string refreshToken, DateTime refreshTokenExpiresOn)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshTokenExpiresOn.ToLocalTime()
            };

            HttpContext.Response.Cookies.Append(GeneralConsts.RefreshTokenKey, refreshToken, cookieOptions);
        }
    }
}

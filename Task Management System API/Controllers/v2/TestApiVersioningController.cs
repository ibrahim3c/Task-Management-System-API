using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_Management_System_API.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion(2.0)]
    [ApiVersion(2.1)]
    public class TestApiVersioningController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion(2.0)]
        public IActionResult TestApiVersioning()
        {
            return Ok("just for testing version 2.0");
        }

        [HttpGet]
        [MapToApiVersion(2.1)]
        public IActionResult TestApiVersioning2()
        {
            return Ok("just for testing version 2.1");
        }

        // ولا كانه موجود
        [HttpGet]
        public IActionResult TestApiVersioning3()
        {
            return Ok("just for testing version test 3");
        }
    }
}

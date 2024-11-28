using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Task_Management_System_API.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion(version: 1.0,Deprecated =true)]
    public class TestApiVersioningController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public TestApiVersioningController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult TestApiVersioning()
        {
            return Ok($" this is just {configuration["test"]} version 1");
        }
    }
}

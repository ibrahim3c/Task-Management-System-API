using Core.Constants;
using Core.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Management_System_API.Services.Interfaces;

namespace Task_Management_System_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Roles =Roles.AdminRole)]
    public class SMSController:ControllerBase
    {
        private readonly ISMSService sMSService;

        public SMSController(ISMSService SMSService)
        {
            sMSService = SMSService;
        }
        [HttpPost]
        public IActionResult SendSMS(SendSMSDTO sendSMSDTO)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = sMSService.SendSMS(sendSMSDTO.PhoneNumber, sendSMSDTO.Body);
            if(!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage.ToString());
            return Ok(result);


        }
    }
}

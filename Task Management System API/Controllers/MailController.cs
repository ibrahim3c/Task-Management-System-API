using Core.Constants;
using Core.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Management_System_API.Services.Interfaces;

namespace Task_Management_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles =Roles.AdminRole)]
    public class MailController:ControllerBase
    {
        private readonly IMaillingService mailService;

        public MailController(IMaillingService mailService)
        {
            this.mailService = mailService;
        }


        [HttpPost("Send")]
        public async Task<IActionResult> SendMailAsync([FromForm]MailRequestDTO mailRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await mailService.SendMailAsync(mailRequest.ToEmail,mailRequest.Subject,mailRequest.Body,mailRequest.Attachments);
            return Ok("Email has been sent successfully.");
        }


       

        [HttpPost("SendBySendGrid")]
        public async Task<IActionResult> SendMailBySendGridAsync([FromForm] MailRequestDTO mailRequest)
        {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await mailService.SendMailBySendGridAsync(mailRequest);
                return Ok("Email has been sent successfully.");
        }
    }
}

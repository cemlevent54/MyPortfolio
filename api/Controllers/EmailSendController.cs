using api.DTOs.Email;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSendController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailSendController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            if (emailRequest == null)
                return BadRequest("Invalid email request");

            bool result = await _emailService.SendEmailAsync(emailRequest);

            if (result)
                return Ok(new { message = "Email sent successfully" });
            else
                return StatusCode(500, "Failed to send email");
        }
    }
}

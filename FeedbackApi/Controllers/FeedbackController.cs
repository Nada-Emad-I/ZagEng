using FeedbackAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
       
        private readonly IConfiguration _configuration;
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(
            IConfiguration configuration,
            ILogger<FeedbackController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

       
        [HttpPost]
        public IActionResult SubmitFeedback(
            [FromQuery] int rate,
            [FromBody] FeedbackBodyDto body)
        {
            
            string systemName = _configuration["SystemSettings:SystemName"]
                                ?? "UnknownSystem";

            
            _logger.LogInformation(
                "Feedback received from user: {UserName}", body.UserName);

            
            if (rate < 3)
            {
                _logger.LogWarning(
                    "Low rating alert! User '{UserName}' rated the service {Rate}/5. Comment: {Comment}",
                    body.UserName, rate, body.Comment);
            }

            
            string message = $"Thank you, {body.UserName}! [{systemName}] has received your Feedback!";

            return Ok(new
            {
                Message = message,
                UserName = body.UserName,
                Rate = rate,
                Comment = body.Comment
            });
        }
    }
}

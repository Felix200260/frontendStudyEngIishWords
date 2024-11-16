using Microsoft.AspNetCore.Mvc;

namespace studyEnglishWords.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}
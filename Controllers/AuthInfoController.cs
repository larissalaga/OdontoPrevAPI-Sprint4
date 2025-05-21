using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OdontoPrevAPI.Controllers
{
    [ApiController]
    [Route("api/authinfo")]
    public class AuthInfoController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var name = User.Identity?.Name ?? "Unknown";
            return Ok(new { name });
        }
    }
}
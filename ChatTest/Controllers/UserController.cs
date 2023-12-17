using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatTest.Controllers
{
    [Route("[controller].[action]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            int userId = GetUserId();
            return Ok(new { userId = userId });
        }

        private int GetUserId()
        {
            string claim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return int.Parse(claim);
        }
    }
}

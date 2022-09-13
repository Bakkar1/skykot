using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkyKotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return Summaries;
        }
    }
}

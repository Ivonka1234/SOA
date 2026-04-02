using Microsoft.AspNetCore.Mvc;

namespace FirstWeek.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { message = "Hello from SOA!" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(new { id, name = "Sample" });
        }
    }
}

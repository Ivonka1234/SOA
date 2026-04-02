using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Week1.Controllers
{
    public class week1 : ControllerBase
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

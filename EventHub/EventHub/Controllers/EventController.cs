using EventHub.Data;
using EventHub.Model;
using EventHub.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        { _eventRepository = eventRepository; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventRepository.GetAllAsync();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Event ev)
        {
            await _eventRepository.AddAsync(ev);
            return CreatedAtAction(nameof(GetAll), new { id = ev.Id }, ev);
        }
    }
}

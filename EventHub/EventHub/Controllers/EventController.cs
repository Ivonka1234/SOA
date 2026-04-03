using EventHub.Data;
using EventHub.Model;
using EventHub.Repository;
using EventHub.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace EventHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService )
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Event ev)
        {
            await _eventService.CreateEventAsync(ev);
            return CreatedAtAction(nameof(GetAll), new { id = ev.Id }, ev);
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming([FromQuery] int days = 7)
        {
           var events= await _eventService.GetUpcomingEventsAsync(days);
            return Ok(events);
        }
        [HttpGet("by-location")]
        public async Task<IActionResult> GetByLocation([FromQuery] string location)
        {
            var events=await _eventService.GetEventsByLocationAsync(location);
            return Ok(events);
        }
        [HttpGet("past")]
        public async Task<IActionResult> GetPast()
        {
            var events=await _eventService.GetPastEventsAsync();
            return Ok(events);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var events=await _eventService.GetEventCountAsync();
            return Ok(events);
        }
        [HttpGet("largest")]
        public async Task<IActionResult> GetLargest()
        {
            var events=await _eventService.GetLargestEventAsync();
            return Ok(events);
        }
        [HttpGet("check-location")]
        public async Task<IActionResult> CheckLocation(
[FromQuery] string location, [FromQuery] DateTime date)
        {
            var events=await _eventService.IsLocationAvailableAsync(location, date);
            return Ok(events);
        }
        [HttpGet("by-organizer/{organizerId}")]
        public async Task<IActionResult> GetByOrganizer(int organizerId)
        {
            var or=await _eventService.GetEventsByOrganizerAsync(organizerId);
            return Ok(or);
        }
        [HttpPut("{id}/assign")]
        public async Task<IActionResult> AssignOrganizer(
     int id, [FromQuery] int organizerId)
        {
            var org=await _eventService.AssignOrganizerAsync(id, organizerId);
            return Ok(org);
        }
    }
}

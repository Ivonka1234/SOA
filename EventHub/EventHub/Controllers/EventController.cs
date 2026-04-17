using EventHub.Data;
using EventHub.DTOs;
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

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var eventEntity = await _eventService.GetEventByIdAsync(id);
            if (eventEntity == null)
                return NotFound();
            return Ok(eventEntity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventCreateDTO newEvent)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _eventService.CreateEventAsync(newEvent);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EventCreateDTO updatedEvent)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updated = await _eventService.UpdateEventAsync(id, updatedEvent);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _eventService.DeleteEventAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming([FromQuery] int days = 7)
        {
            var upcoming = await _eventService.GetUpcomingEventsAsync(days);
            return Ok(upcoming);
        }

        [HttpGet("by-location")]
        public async Task<IActionResult> GetByLocation([FromQuery] string location)
        {
            var events = await _eventService.GetEventsByLocationAsync(location);
            return Ok(events);
        }

        [HttpGet("past")]
        public async Task<IActionResult> GetPast()
        {
            var past = await _eventService.GetPastEventsAsync();
            return Ok(past);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _eventService.GetEventCountAsync();
            return Ok(count);
        }

        [HttpGet("largest")]
        public async Task<IActionResult> GetLargest()
        {
            var largest = await _eventService.GetLargestEventAsync();
            if (largest == null) return NotFound();
            return Ok(largest);
        }

        [HttpGet("check-location")]
        public async Task<IActionResult> CheckLocation(
            [FromQuery] string location, [FromQuery] DateTime date)
        {
            var available = await _eventService.IsLocationAvailableAsync(location, date);
            return Ok(available);
        }

        [HttpGet("by-organizer/{organizerId}")]
        public async Task<IActionResult> GetByOrganizer(int organizerId)
        {
            var events = await _eventService.GetEventsByOrganizerAsync(organizerId);
            return Ok(events);
        }

        [HttpPut("{id}/assign")]
        public async Task<IActionResult> AssignOrganizer(
            int id, [FromQuery] int organizerId)
        {
            try
            {
                await _eventService.AssignOrganizerAsync(id, organizerId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

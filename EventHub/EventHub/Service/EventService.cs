using EventHub.Data;
using EventHub.Model;
using EventHub.Repository;
using System.Security.AccessControl;

namespace EventHub.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IOrganizerRepository _organizerRepository;
        public EventService(IEventRepository eventRepository, IOrganizerRepository organizerRepository  )
        {
            _eventRepository = eventRepository;
            _organizerRepository = organizerRepository;
        }

        public async Task<bool> AssignOrganizerAsync(int eventId, int organizerId)
        {

            var ev = await _eventRepository.GetByIdAsync(eventId);
            if (ev == null)
            {
                throw new KeyNotFoundException("Event not found");
            }

            var organizer = await _organizerRepository.GetByIdAsync(organizerId);
            if (organizer == null)
            {
                throw new KeyNotFoundException("Organizer not found");
            }

            ev.OrganizerId = organizerId;

            await _eventRepository.UpdateAsync(ev);

            return true;
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            if (string.IsNullOrWhiteSpace(newEvent.Title))
            {
                throw new ArgumentNullException("Event title cannot be null");
            }
            if (newEvent.Date < DateTime.UtcNow)
            {
                throw new ArgumentException("Event date must be in the future");
            }
            if (newEvent.MaxCapacity <= 0)
            {
                throw new ArgumentException("Max capacity must be positive");
            }
            await _eventRepository.AddAsync(newEvent);
            return newEvent;
        }

        public async Task DeleteEventAsync(int id)
        {
           await _eventRepository.DeleteAsync(id);
          
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task<int> GetEventCountAsync()
        {
            var allEvents=await _eventRepository.GetAllAsync();
            return allEvents.Count();
        }

        public async Task<IEnumerable<Event>> GetEventsByLocationAsync(string location)
        {
            var allEvents=await _eventRepository.GetAllAsync();
           return allEvents.Where(e=>e.Location==location).ToList();
        }

        public async Task<IEnumerable<Event>> GetEventsByOrganizerAsync(int organizerId)
        {
            var allEvents=await _eventRepository.GetAllAsync();
            return allEvents.Where(e=>e.OrganizerId==organizerId).ToList();
        }

        public async Task<Event?> GetLargestEventAsync()
        {
           var allEvents=await _eventRepository.GetAllAsync();
            return allEvents.OrderByDescending(e => e.MaxCapacity).FirstOrDefault();
        }

        public async Task<IEnumerable<Event>> GetPastEventsAsync()
        {
          var allEvents=await _eventRepository.GetAllAsync();
            return allEvents.Where(e => e.Date < DateTime.Now).OrderBy(e => e.Date);
        }

        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync(int days)
        {
            var allEvents= await _eventRepository.GetAllAsync();
            return allEvents.Where(e=>e.Date>DateTime.Now && e.Date<=DateTime.Now.AddDays(days)).OrderBy(e=>e.Date);
        }

        public async Task<bool> IsLocationAvailableAsync(string location, DateTime date)
        {
            var allEvents=await _eventRepository.GetAllAsync();
            return !allEvents.Any(e=>e.Location==location&& e.Date.Date==date.Date);
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            if (string.IsNullOrWhiteSpace(updatedEvent.Title))
            {
                throw new ArgumentNullException("Event title cannot be null");
            }
            if (updatedEvent.Date < DateTime.UtcNow)
            {
                throw new ArgumentException("Event date must be in the future");
            }
            if (updatedEvent.MaxCapacity <= 0)
            {
                throw new ArgumentException("Max capacity must be positive");
            }
            await _eventRepository.UpdateAsync(updatedEvent);
        }
    }
}

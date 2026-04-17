using AutoMapper;
using EventHub.Data;
using EventHub.DTOs;
using EventHub.Model;
using EventHub.Repository;
using System.Security.AccessControl;

namespace EventHub.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IMapper _mapper;


        public EventService(IEventRepository eventRepository, IOrganizerRepository organizerRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _organizerRepository = organizerRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();   
            return _mapper.Map<IEnumerable<EventDTO>>(events); 
        }


        public async Task<EventDTO?> GetEventByIdAsync(int id)
        {
            var entity = await _eventRepository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<EventDTO>(entity);
        }

        public async Task<EventDTO> CreateEventAsync(EventCreateDTO newEvent)
        {
           
            if (string.IsNullOrWhiteSpace(newEvent.Title))
                throw new ArgumentNullException("Event title cannot be empty");
            if (newEvent.Date < DateTime.Now)
                throw new ArgumentException("Event Date must be in the future");
            if (newEvent.MaxCapacity <= 0)
                throw new ArgumentException("Max capacity must be positive");
            var entity = _mapper.Map<Event>(newEvent);
            await _eventRepository.AddAsync(entity);
            return _mapper.Map<EventDTO>(entity);
        }

        public async Task<EventDTO?> UpdateEventAsync(int id, EventCreateDTO updatedEvent)
        {
            if (string.IsNullOrWhiteSpace(updatedEvent.Title))
                throw new ArgumentNullException("Event title cannot be empty");
    
            if (updatedEvent.Date < DateTime.Now)
                throw new ArgumentException("Event Date must be in the future");
      
            if (updatedEvent.MaxCapacity <= 0)
                throw new ArgumentException("Max capacity must be positive");

            var existing = await _eventRepository.GetByIdAsync(id);
            if (existing == null) return null;
            _mapper.Map(updatedEvent, existing);
            await _eventRepository.UpdateAsync(existing);
            return _mapper.Map<EventDTO>(existing);
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var entity = await _eventRepository.GetByIdAsync(id);
            if (entity == null) return false;
            await _eventRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<EventDTO>> GetUpcomingEventsAsync(int days)
        {
            var allEvents = await _eventRepository.GetAllAsync();
            var filtered = allEvents.Where(e => e.Date > DateTime.Now && e.Date <= DateTime.Now.AddDays(days))
                .OrderBy(e => e.Date);

            return _mapper.Map<IEnumerable<EventDTO>>(filtered);

        }

        public async Task<IEnumerable<EventDTO>> GetEventsByLocationAsync(string location)
        {
            var allEvents = await _eventRepository.GetAllAsync();
            var filteredresult = allEvents.Where(e => e.Location.ToLower() == location.ToLower());
            return _mapper.Map<IEnumerable<EventDTO>>(filteredresult);
        }

        public async Task<IEnumerable<EventDTO>> GetPastEventsAsync()
        {
            var allEvents = await _eventRepository.GetAllAsync();
            var results = allEvents.Where(e => e.Date < DateTime.Now).OrderBy(e => e.Date);
            return _mapper.Map<IEnumerable<EventDTO>>(results);
        }

        public async Task<int> GetEventCountAsync()
        {
            var allEvents = await _eventRepository.GetAllAsync();
            return allEvents.Count();
        }

        public async Task<EventDTO?> GetLargestEventAsync()
        {
            var allEvents = await _eventRepository.GetAllAsync();
            var largest = allEvents.OrderByDescending(e => e.MaxCapacity).FirstOrDefault();
            if (largest == null) return null;
            return _mapper.Map<EventDTO>(largest);
        }

        public async Task<bool> IsLocationAvailableAsync(string location, DateTime date)
        {
            var allEvents = await _eventRepository.GetAllAsync();
            return !allEvents.Any(e => e.Location == location && e.Date.Date == date.Date);
        }

        public async Task<IEnumerable<EventDTO>> GetEventsByOrganizerAsync(int organizerId)
        {
            var allEvents = await _eventRepository.GetAllAsync();
            var filtered = allEvents.Where(e => e.OrganizerId == organizerId);
            return _mapper.Map<IEnumerable<EventDTO>>(filtered);

        }

        public async Task AssignOrganizerAsync(int eventId, int organizerId)
        {
            var events = await _eventRepository.GetByIdAsync(eventId);
            if (events == null)
                throw new ArgumentException("Event does not exist");
            var organizer = await _organizerRepository.GetByIdAsync(organizerId);
            if (organizer == null)
                throw new ArgumentException("Organizer does not exist");
            events.OrganizerId = organizerId;
            await _eventRepository.UpdateAsync(events);
        }
    }
}

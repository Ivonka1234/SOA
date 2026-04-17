using EventHub.DTOs;
using EventHub.Model;

namespace EventHub.Service
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllEventsAsync();
        Task<EventDTO?> GetEventByIdAsync(int id);
        Task<EventDTO> CreateEventAsync(EventCreateDTO newEvent);
        Task<EventDTO?> UpdateEventAsync(int id, EventCreateDTO updatedEvent);
        Task<bool> DeleteEventAsync(int id);
        Task<IEnumerable<EventDTO>> GetUpcomingEventsAsync(int days);
        Task<IEnumerable<EventDTO>> GetEventsByLocationAsync(string location);
        Task<IEnumerable<EventDTO>> GetPastEventsAsync();
        Task<int> GetEventCountAsync();
        Task<EventDTO?> GetLargestEventAsync();
        Task<bool> IsLocationAvailableAsync(string location, DateTime date);
        Task<IEnumerable<EventDTO>> GetEventsByOrganizerAsync(int organizerId);
        Task AssignOrganizerAsync(int eventId, int organizerId);

    }
}

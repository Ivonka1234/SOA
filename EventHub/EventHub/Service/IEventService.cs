using EventHub.Model;

namespace EventHub.Service
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?>GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event newEvent);
        Task UpdateEventAsync(Event updatedEvent);
        Task DeleteEventAsync(int id);
        Task<IEnumerable<Event>> GetUpcomingEventsAsync(int days);
        Task<IEnumerable<Event>> GetEventsByLocationAsync(string location);
        Task<IEnumerable<Event>> GetPastEventsAsync();
        Task<int> GetEventCountAsync();
        Task<Event?> GetLargestEventAsync();
        Task<bool> IsLocationAvailableAsync(string location, DateTime date);
        Task<IEnumerable<Event>> GetEventsByOrganizerAsync(int organizerId);
        Task<bool> AssignOrganizerAsync(int eventId, int organizerId);
        
    }
}

using EventHub.Model;

namespace EventHub.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task AddAsync(Event entity);

        Task<Event?> GetByIdAsync(int id);
        Task UpdateAsync(Event entity);
        Task DeleteAsync(int id);
    }
}

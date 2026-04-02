using EventHub.Model;

namespace EventHub.Repository
{
    public interface IOrganizerRepository
    {
        Task<IEnumerable<Organizer>> GetAllAsync();
        Task AddAsync(Organizer entity);

        Task<Organizer?> GetByIdAsync(int id);
        Task UpdateAsync(Organizer entity);
        Task DeleteAsync(int id);
    }
}

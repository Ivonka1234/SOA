using EventHub.Data;
using EventHub.Model;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        { _context = context; }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task AddAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task UpdateAsync(Event entity)
        {
            _context.Events.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // 1. Find that the event exists
            var entity = await _context.Events.FindAsync(id);
            // 2. Remove the found entity
            if (entity != null)
            {
                _context.Events.Remove(entity);
                // 3. Save Change
                await _context.SaveChangesAsync();
            }

        }
    }
}

using EventHub.Data;
using EventHub.Model;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Repository
{
    public class OrganizerRepository:IOrganizerRepository
    {
        private readonly AppDbContext _context;

        public OrganizerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organizer>> GetAllAsync()
        {
            return await _context.Organizers.ToListAsync();
        }

        public async Task AddAsync(Organizer entity)
        {
            await _context.Organizers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Organizer?> GetByIdAsync(int id)
        {
            return await _context.Organizers.FindAsync();
        }

        public async Task UpdateAsync(Organizer entity)
        {
            _context.Organizers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer != null)
            {
                _context.Organizers.Remove(organizer);
                await _context.SaveChangesAsync();

            }
        }
    }
}

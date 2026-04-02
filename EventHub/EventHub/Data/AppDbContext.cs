
using Microsoft.EntityFrameworkCore;
using EventHub.Model;

namespace EventHub.Data

   
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public  DbSet<Event> Events {  get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Organizer>().HasData(
     new Organizer
     {
         Id = 5,
         Name = "Alice",
         Email = "alice@uni.edu",
         Department = "CS"
     }
 );

            mb.Entity<Event>().HasData(
                new Event
                {
                    Id = 5,
                    Title = "Hackathon 2026",
                    Date = new DateTime(2026, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                    OrganizerId = 5, 
                    Location = "Main Hall",
                    MaxCapacity = 200
                }
            );
        }
    }
}

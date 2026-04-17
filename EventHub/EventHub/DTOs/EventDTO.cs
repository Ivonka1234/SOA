using EventHub.Model;
using System.ComponentModel.DataAnnotations;

namespace EventHub.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int MaxCapacity { get; set; }
        public string OrganizerName { get; set; }
    }
}

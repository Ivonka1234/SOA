

using System.ComponentModel.DataAnnotations;

namespace EventHub.Model
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required, StringLength(100)]
        public string Location {  get; set; }
        [Range(1, 5000)]
        public int MaxCapacity {  get; set; }
        public int? OrganizerId { get; set;}
        public Organizer? Organizer { get; set;}
    }
}

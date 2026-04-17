using System.ComponentModel.DataAnnotations;

namespace EventHub.DTOs
{
    public class EventCreateDTO
    {
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Location { get; set; }
        [Range(1,100000)]
        public int MaxCapacity { get; set; }
        public int OrganizerId { get; set; }
    }
}

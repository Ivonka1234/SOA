using System.ComponentModel.DataAnnotations;

namespace EventHub.Model
{
    public class Organizer
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(100)]
        public string Name { get; set; }

        [Required,DataType(DataType.EmailAddress)]
        public string Email{ get; set; }
        [StringLength(100)]
        public string Department { get; set; }

        public ICollection <Event> Events { get; set; }
    }
}

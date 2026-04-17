using AutoMapper;
using EventHub.DTOs;
using EventHub.Model;

namespace EventHub.Profiles
{
    public class EventProfile:Profile
    {
  public EventProfile() {
            CreateMap<Event, EventDTO>()
            .ForMember(
                dest => dest.OrganizerName,
                opt => opt.MapFrom(src => src.Organizer != null
                    ? src.Organizer.Name : "")
            );
            CreateMap<EventCreateDTO, Event>(); 
        }

    }
}

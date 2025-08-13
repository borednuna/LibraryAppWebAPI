using AutoMapper;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Enums;

namespace LibraryAppWebAPI.MappingProfiles
{
    // public class MemberMappingProfile : Profile
    // {
    //     public MemberMappingProfile()
    //     {
    //         CreateMap<Member, MemberDto>();

    //         CreateMap<MemberDto, Member>()
    //             .ForMember(dest => dest.Id, opt => opt.Ignore()) 
    //             .ForMember(dest => dest.MembershipId, opt => opt.MapFrom(src => Guid.NewGuid()))
    //             .ForMember(dest => dest.MembershipDate, opt => opt.MapFrom(src => DateTime.UtcNow))
    //             .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
    //             .ForMember(dest => dest.BorrowingHistory, opt => opt.Ignore());
    //     }
    // }
}

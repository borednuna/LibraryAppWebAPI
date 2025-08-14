using AutoMapper;
using LibraryAppWebAPI.DTOs;
using LibraryAppWebAPI.Models;

namespace LibraryAppWebAPI.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookDtoResponse>();

        CreateMap<BookDtoRequest, Book>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.BorrowingHistory, opt => opt.Ignore());
    }
}

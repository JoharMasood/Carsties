using Contracts;
using AutoMapper;
using SearchService.Models;
namespace SearchService;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AuctionCreated, Item>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id));

        CreateMap<AuctionUpdated, Item>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Make))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage));
    }
}
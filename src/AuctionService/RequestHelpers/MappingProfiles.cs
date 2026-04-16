using AutoMapper;
using AuctionService.Entities;
using AuctionService.DTOs;
using Contracts;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
        CreateMap<Item, AuctionDto>();
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s));
        CreateMap<CreateAuctionDto, Item>();
        // CreateMap<AuctionDto, AuctionCreated>();
        CreateMap<AuctionDto, AuctionCreated>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Make))
    .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
    .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
    .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
    .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Mileage))
    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
    }

}
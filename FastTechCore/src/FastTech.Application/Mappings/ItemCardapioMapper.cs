using AutoMapper;
using FastTech.Domain.Entities;
using DTO = FastTech.Application.DataTransferObjects;
using MSG = FastTech.Application.DataTransferObjects.MessageBrokers;

namespace FastTech.Application.Mappings;

public class ItemCardapioMapper : Profile
{
    public ItemCardapioMapper()
    {
        CreateMap<ItemCardapio, DTO.ItemCardapio>()
            .ConstructUsing(src => new DTO.ItemCardapio())
            .ReverseMap()
            .ConstructUsing(src => new ItemCardapio())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.RemovedAt, opt => opt.Ignore())
            .ForMember(dest => dest.RemovedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Removed, opt => opt.Ignore());

        CreateMap<DTO.BasicItemCardapio, ItemCardapio>()
            .ConstructUsing(src => new ItemCardapio());

        CreateMap<ItemCardapio, MSG.ItemCardapio>()
            .ReverseMap()
            .ConstructUsing(src => new ItemCardapio())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.RemovedAt, opt => opt.Ignore())
            .ForMember(dest => dest.RemovedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Removed, opt => opt.Ignore());

        CreateMap<MSG.BasicItemCardapio, ItemCardapio>()
            .ConstructUsing(src => new ItemCardapio());
    }
}
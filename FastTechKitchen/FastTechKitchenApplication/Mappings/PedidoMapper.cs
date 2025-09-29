using AutoMapper;
using FastTechKitchen.Domain.Entities;
using DTO = FastTechKitchen.Application.DataTransferObjects;
using MSG = FastTechKitchen.Application.DataTransferObjects.MessageBrokers;


namespace FastTechKitchen.Application.Mappings
{
    public class PedidoMapper : Profile
    {
        public PedidoMapper()
        {
            CreateMap<Pedido, DTO.Pedido>()
            .ConstructUsing(src => new DTO.Pedido())
            .ReverseMap()
            .ConstructUsing(src => new Pedido())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.RemovedAt, opt => opt.Ignore())
            .ForMember(dest => dest.RemovedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Removed, opt => opt.Ignore());

            CreateMap<DTO.BasicPedido, Pedido>()
                .ForMember(dest => dest.Itens, opt => opt.Ignore())
                .ConstructUsing(src => new Pedido());

            CreateMap<Pedido, MSG.Pedido>()
                .ReverseMap()
                .ConstructUsing(src => new Pedido())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.RemovedAt, opt => opt.Ignore())
                .ForMember(dest => dest.RemovedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Removed, opt => opt.Ignore());

            CreateMap<MSG.BasicPedido, Pedido>()
                .ConstructUsing(src => new Pedido());
        }
    }
}

//using AutoMapper;
//using FastTechKitchen.Domain.Entities;
//using DTO = FastTechKitchen.Application.DataTransferObjects;
//using MSG = FastTechKitchen.Application.DataTransferObjects.MessageBrokers;
using AutoMapper;
using FastTechKitchen.Domain.Entities;
using CTT = FastTech.Contracts.DataTransferObjects; // Alias para os Contratos
using DTO = FastTechKitchen.Application.DataTransferObjects; // Alias para os DTOs internos da Kitchen

namespace FastTechKitchen.Application.Mappings
{
    public class PedidoMapper : Profile
    {
        public PedidoMapper()
        {
            // Mapeamento Principal: Entidade <-> DTO (para Controllers e retorno do App Service)
            CreateMap<Pedido, DTO.Pedido>().ReverseMap();
            CreateMap<PedidoItemCardapio, DTO.PedidoItemCardapio>().ReverseMap();

            // Mapeamento de CONTRATO (FastTech.Contracts) para ENTIDADE (Domain)

            // CONTRATO BÁSICO (BasicPedido) -> ENTIDADE PEDIDO
            CreateMap<CTT.BasicPedido, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Itens, opt => opt.Ignore())
                .ConstructUsing(src => new Pedido());

            // CONTRATO ITEM BÁSICO (BasicPedidoItemCardapio) -> ENTIDADE PEDIDO ITEM CARDAPIO
            CreateMap<CTT.BasicPedidoItemCardapio, PedidoItemCardapio>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PedidoId, opt => opt.Ignore());

            // Mantenha mapeamentos de DTOs internos se necessário, ou remova se forem legados:
            // CreateMap<DTO.BasicPedido, Pedido>().ForMember(dest => dest.Itens, opt => opt.Ignore());
            // CreateMap<MSG.BasicPedido, Pedido>().ConstructUsing(src => new Pedido());

            //CreateMap<Pedido, DTO.Pedido>()
            //.ConstructUsing(src => new DTO.Pedido())
            //.ReverseMap()
            //.ConstructUsing(src => new Pedido())
            //.ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            //.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            //.ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            //.ForMember(dest => dest.RemovedAt, opt => opt.Ignore())
            //.ForMember(dest => dest.RemovedBy, opt => opt.Ignore())
            //.ForMember(dest => dest.Removed, opt => opt.Ignore());

            //CreateMap<DTO.BasicPedido, Pedido>()
            //    .ForMember(dest => dest.Itens, opt => opt.Ignore())
            //    .ConstructUsing(src => new Pedido());

            //CreateMap<Pedido, MSG.Pedido>()
            //    .ReverseMap()
            //    .ConstructUsing(src => new Pedido())
            //    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            //    .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.RemovedAt, opt => opt.Ignore())
            //    .ForMember(dest => dest.RemovedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.Removed, opt => opt.Ignore());

            //CreateMap<MSG.BasicPedido, Pedido>()
            //    .ConstructUsing(src => new Pedido());
        }
    }
}

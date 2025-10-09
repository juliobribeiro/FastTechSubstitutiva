using AutoMapper;
using EN = FastTechKitchen.Domain.Entities;
using CTT = FastTech.Contracts.DataTransferObjects; // Alias para os Contratos
using DTO = FastTechKitchen.Application.DataTransferObjects; // Alias para os DTOs internos da Kitchen

namespace FastTechKitchen.Application.Mappings
{
    // Esta é a classe correta. Removemos a aninhada.
    public class PedidoMapper : Profile
    {
        // O construtor deve estar aqui.
        public PedidoMapper()
        {
            // Mapeamento Principal: Entidade <-> DTO (para Controllers e retorno do App Service)
            CreateMap<EN.Pedido, DTO.Pedido>().ReverseMap();
            CreateMap<EN.PedidoItemCardapio, DTO.PedidoItemCardapio>().ReverseMap();

            // ⚠️ MAPA CRÍTICO QUE ESTAVA FALTANDO OU COM FALHA NO CARREGAMENTO
            // CONTRATO BÁSICO (CTT.BasicPedido) -> ENTIDADE PEDIDO (Pedido)
            // Se BasicPedido não tiver ClientId, você terá um CS1061 ou erro de runtime.
            CreateMap<CTT.BasicPedido, EN.Pedido>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                // Mapeamento Explícito
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.FormaDeEntrega, opt => opt.MapFrom(src => src.FormaDeEntrega))
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
                .ConstructUsing(src => new EN.Pedido());

            // CONTRATO ITEM BÁSICO (CTT.BasicPedidoItemCardapio) -> ENTIDADE PEDIDO ITEM CARDAPIO (PedidoItemCardapio)
            // Necessário para o loop interno no Consumer
            CreateMap<CTT.BasicPedidoItemCardapio, EN.PedidoItemCardapio>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PedidoId, opt => opt.Ignore());

            // Mapeamento de EN.Pedido para DTO.Pedido (o último mapeamento no consumer) já está acima, mas garantimos que não está duplicado.
        }
    }
}
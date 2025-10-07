using System;
using FastTechKitchen.Domain.Entities; // Garanta que este using exista

namespace FastTechKitchen.Domain.Entities
{
    public class PedidoItemCardapio : BaseEntity
    {
        public Guid PedidoId { get; set; }
        public Guid ItemCardapioId { get; set; }

        // ⚠️ CORREÇÃO CRÍTICA: ADICIONE AS PROPRIEDADES DE NAVEGAÇÃO
        public Pedido Pedido { get; set; }
        // Assumindo que você tem uma Entidade ItemCardapio no domínio
        public ItemCardapio ItemCardapio { get; set; }

        public PedidoItemCardapio() : base() { }

        // Mantenha o construtor:
        public PedidoItemCardapio(Guid pedidoId, Guid itemCardapioId, bool ativo, Guid userId) : base()
        {
            PedidoId = pedidoId;
            ItemCardapioId = itemCardapioId;

            PrepareToInsert(userId);
        }
    }
}
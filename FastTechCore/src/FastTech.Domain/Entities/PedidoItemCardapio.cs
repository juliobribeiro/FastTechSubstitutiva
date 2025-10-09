using System;
using FastTech.Domain.Entities;
using FastTechKitchen.Domain.Entities; // Garanta que este using exista

namespace FastTechKitchen.Domain.Entities
{
    public class PedidoItemCardapio : BaseEntity
    {
        public Guid PedidoId { get; set; }
        public Guid ItemCardapioId { get; set; }
                
        public Pedido Pedido { get; set; }
        public ItemCardapio ItemCardapio { get; set; }
        public int Quantidade { get; set; }

        public PedidoItemCardapio() : base() { }

        public PedidoItemCardapio(Guid pedidoId, Guid itemCardapioId, int quantidade, Guid userId) : base()
        {
            PedidoId = pedidoId;
            ItemCardapioId = itemCardapioId;
            Quantidade = quantidade;

            PrepareToInsert(userId);
        }
    }
}
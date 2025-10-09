using FastTechKitchen.Domain.Entities;

namespace FastTech.Domain.Entities
{
    public class Pedido : BaseEntity
    {
        public Guid PedidoId { get; set; }
        public int FormaDeEntrega { get; set; }
        public bool Ativo { get; set; }
        public List<PedidoItemCardapio> ItensCardapio { get; set; } = new();
       

        public Pedido() : base() { }

        //public Pedido(Guid itemCardapioId, int formaDeEntrega, bool ativo, Guid userId) : base()
        //{
        //    ItemCardapioId = itemCardapioId;
        //    FormaDeEntrega = formaDeEntrega;
        //    Ativo = ativo;

        //    PrepareToInsert(userId);
        //}
        public Pedido(int formaDeEntrega, bool ativo, Guid userId) : base()
        {
            FormaDeEntrega = formaDeEntrega;
            Ativo = ativo;

            PrepareToInsert(userId);

        }

    }
}

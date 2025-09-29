namespace FastTechKitchen.Domain.Entities
{
    public class PedidoItemCardapio : BaseEntity
    {
        public Guid PedidoId { get; set; }
        public Guid ItemCardapioId { get; set; }

        public PedidoItemCardapio() : base() { }

        public PedidoItemCardapio(Guid pedidoId, Guid itemCardapioId, bool ativo, Guid userId) : base()
        {
            PedidoId = pedidoId;
            ItemCardapioId = itemCardapioId;

            PrepareToInsert(userId);
        }
    }
}

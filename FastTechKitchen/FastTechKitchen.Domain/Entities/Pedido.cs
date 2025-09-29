namespace FastTechKitchen.Domain.Entities
{
    public class Pedido : BaseEntity
    {
        public int FormaDeEntrega { get; set; }
        public bool Ativo { get; set; }

        // Relação 1:N com PedidoItemCardapio
        public List<PedidoItemCardapio> Itens { get; set; } = new();

        public Pedido() : base() { }

        public Pedido(int formaDeEntrega, bool ativo, Guid userId) : base()
        {
            FormaDeEntrega = formaDeEntrega;
            Ativo = ativo;

            PrepareToInsert(userId);
        }
    }
}

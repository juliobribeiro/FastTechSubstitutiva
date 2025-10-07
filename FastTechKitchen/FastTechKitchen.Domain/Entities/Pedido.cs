namespace FastTechKitchen.Domain.Entities
{
    public class Pedido : BaseEntity
    {
        public int FormaDeEntrega { get; set; }
        public bool Ativo { get; set; }

        public Guid? ClienteId { get; set; } // Permitindo nullable se for opcional no Contrato

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

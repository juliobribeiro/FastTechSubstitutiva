namespace FastTechKitchen.Domain.Entities;

public class ItemCardapio : BaseEntity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Preco { get; set; }
    public bool Disponivel { get; set; }

    // Relação 1:N com PedidoItemCardapio
    public List<PedidoItemCardapio> Itens { get; set; } = new();

    public ItemCardapio() : base() { }

    public ItemCardapio(string nome, string descricao, double preco, bool disponivel, Guid userId) : base()
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Disponivel = disponivel;

        PrepareToInsert(userId);
    }
}

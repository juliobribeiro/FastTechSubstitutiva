namespace FastTech.Domain.Entities;

public class ItemCardapio : BaseEntity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Preco { get; set; }
    public bool Disponivel { get; set; }

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

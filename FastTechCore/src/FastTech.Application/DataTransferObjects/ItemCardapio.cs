using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastTech.Application.DataTransferObjects
{
    public class ItemCardapio : BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
        public string Nome { get; set; }

        [JsonPropertyName("descricao")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [StringLength(300, ErrorMessage = "A descrição pode ter no máximo 300 caracteres.")]
        public string Descricao { get; set; }

        [JsonPropertyName("preco")]
        [Required(ErrorMessage = "O campo Preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public double Preco { get; set; }

        [JsonPropertyName("disponivel")]
        public bool Disponivel { get; set; }
        public ItemCardapio() : base() { }

        public ItemCardapio(Guid id, string nome, string descricao, double preco, bool disponivel,
                            DateTime createdAt, Guid createdBy,
                            DateTime? updatedAt, Guid? updatedBy,
                            bool removed, DateTime? removedAt, Guid? removedBy)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Disponivel = disponivel;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
            Removed = removed;
            RemovedAt = removedAt;
            RemovedBy = removedBy;
        }
    }
}

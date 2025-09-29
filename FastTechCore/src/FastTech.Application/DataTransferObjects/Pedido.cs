using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastTech.Application.DataTransferObjects
{
    public class Pedido : BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [JsonPropertyName("itemCardapioId")]
        [Required(ErrorMessage = "O campo ItemCardapioId é obrigatório.")]
        public Guid ItemCardapioId { get; set; }

        [JsonPropertyName("formaDeEntrega")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public int FormaDeEntrega { get; set; }

        [JsonPropertyName("ativo")]
        [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
        public bool Ativo { get; set; }
        public Pedido() : base() { }

        public Pedido(Guid id, Guid itemCardapioId, int formaDeEntrega, bool ativo,
                            DateTime createdAt, Guid createdBy,
                            DateTime? updatedAt, Guid? updatedBy,
                            bool removed, DateTime? removedAt, Guid? removedBy)
        {
            Id = id;
            ItemCardapioId = itemCardapioId;
            FormaDeEntrega = formaDeEntrega;
            Ativo = ativo;
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

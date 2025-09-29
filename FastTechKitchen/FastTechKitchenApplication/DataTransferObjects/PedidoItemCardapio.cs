using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastTechKitchen.Application.DataTransferObjects
{
    public class PedidoItemCardapio : BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [JsonPropertyName("itemCardapioId")]
        [Required(ErrorMessage = "O campo ItemCardapioId é obrigatório.")]
        public Guid ItemCardapioId { get; set; }

        [JsonPropertyName("pedidoId")]
        [Required(ErrorMessage = "O campo PedidoId é obrigatório.")]
        public Guid PedidoId { get; set; }
        public PedidoItemCardapio() : base() { }

        public PedidoItemCardapio(Guid id, Guid itemCardapioId, Guid pedidoId, bool ativo,
                            DateTime createdAt, Guid createdBy,
                            DateTime? updatedAt, Guid? updatedBy,
                            bool removed, DateTime? removedAt, Guid? removedBy)
        {
            Id = id;
            ItemCardapioId = itemCardapioId;
            PedidoId = pedidoId;
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

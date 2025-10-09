using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastTech.Application.DataTransferObjects
{
    public class Pedido : BaseModel
    {
        [Key]
        public Guid PedidoId { get; set; }

        [JsonPropertyName("itensCardapio")]
        [Required(ErrorMessage = "O campo ItensCardapio é obrigatório.")]
        public List<ItemCardapio> ItensCardapio { get; set; }

        [JsonPropertyName("formaDeEntrega")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public int FormaDeEntrega { get; set; }

        [JsonPropertyName("ativo")]
        [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
        public bool Ativo { get; set; }
        public Pedido() : base() { }

        public Pedido(Guid pedidoId, List<ItemCardapio> itensCardapioId, int formaDeEntrega, bool ativo,
                            DateTime createdAt, Guid createdBy,
                            DateTime? updatedAt, Guid? updatedBy,
                            bool removed, DateTime? removedAt, Guid? removedBy)
        {
            PedidoId = pedidoId;
            ItensCardapio = itensCardapioId;
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

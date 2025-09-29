using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastTechKitchen.Application.DataTransferObjects
{
    public class BasicPedidoItemCardapio
    {
        [JsonPropertyName("PedidoId")]
        [Required(ErrorMessage = "O campo PedidoId é obrigatório.")]
        public Guid PedidoId { get; set; }

        [JsonPropertyName("ItemCardapioId")]
        [Required(ErrorMessage = "O campo ItemCardapioId é obrigatório.")]
        public Guid ItemCardapioId { get; set; }

        public BasicPedidoItemCardapio() { }

        public BasicPedidoItemCardapio(Guid pedidoId, Guid itemCardapioId)
        {
            PedidoId = pedidoId;
            ItemCardapioId = itemCardapioId;
        }
    }
}

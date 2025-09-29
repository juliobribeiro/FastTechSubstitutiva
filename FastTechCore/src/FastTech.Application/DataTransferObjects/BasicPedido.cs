using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastTech.Application.DataTransferObjects
{
    public class BasicPedido
    {
        [JsonPropertyName("ItemCardapioId")]
        [Required(ErrorMessage = "O campo ItemCardapioId é obrigatório.")]
        public Guid ItemCardapioId { get; set; }

        [JsonPropertyName("FormaDeEntrega")]
        [Required(ErrorMessage = "O campo FormaDeEntrega é obrigatório.")]
        public int FormaDeEntrega { get; set; }

        [JsonPropertyName("Ativo")]
        public bool Ativo { get; set; } = true;

        public BasicPedido() : base() { }

        public BasicPedido(Guid itemCardapioId, int formaDeEntrega, bool ativo)
        {
            ItemCardapioId = itemCardapioId;
            FormaDeEntrega = formaDeEntrega;
            Ativo = ativo;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastTechKitchen.Application.DataTransferObjects
{
    public class BasicPedido
    {
        [Required(ErrorMessage = "O campo FormaDeEntrega é obrigatório.")]
        [JsonPropertyName("FormaDeEntrega")]
        public int FormaDeEntrega { get; set; }

        [JsonPropertyName("Ativo")]
        public bool Ativo { get; set; } = true;

        [Required(ErrorMessage = "Deve conter pelo menos um item de cardápio.")]
        [MinLength(1, ErrorMessage = "Deve conter pelo menos um item de cardápio.")]
        [JsonPropertyName("Itens")]
        public List<BasicPedidoItemCardapio> Itens { get; set; } = new();

        public BasicPedido() { }

        public BasicPedido(int formaDeEntrega, bool ativo, List<BasicPedidoItemCardapio> itens)
        {
            FormaDeEntrega = formaDeEntrega;
            Ativo = ativo;
            Itens = itens;
        }
    }
}

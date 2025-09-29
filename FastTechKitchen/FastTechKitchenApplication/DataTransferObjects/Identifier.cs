using System.Text.Json.Serialization;

namespace FastTechKitchen.Application.DataTransferObjects
{
    public class Identifier
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        public Identifier() { }
    }
}
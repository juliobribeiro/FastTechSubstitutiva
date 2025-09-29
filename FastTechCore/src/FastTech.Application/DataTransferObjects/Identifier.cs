using System.Text.Json.Serialization;

namespace FastTech.Application.DataTransferObjects
{
    public class Identifier
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        public Identifier() { }
    }
}
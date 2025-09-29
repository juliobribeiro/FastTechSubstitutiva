using FastTech.Domain.Enums;
using System.Text.Json.Serialization;

namespace FastTech.Application.DataTransferObjects;

public class User : BaseModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("permission_level")]
    public PermissionLevel PermissionLevel { get; set; }

    [JsonPropertyName("ddd")]
    public int DDD { get; set; }

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }

    public User() : base() { }
}
using FastTechKitchen.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FastTechKitchen.Application.DataTransferObjects;

public class BasicUser
{
    [JsonPropertyName("name")]
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s'-]+$", ErrorMessage = "O nome pode conter apenas letras e espaços.")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    [Required(ErrorMessage = "A senha é obrigatória.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "A senha deve ter pelo menos 8 caracteres, incluindo uma letra maiúscula, um número e um caractere especial.")]
    public string Password { get; set; }

    [JsonPropertyName("permission_level")]
    [Required(ErrorMessage = "O campo Level de Permissão é obrigatório.")]
    public PermissionLevel PermissionLevel { get; set; }

    [JsonPropertyName("ddd")]
    [Required(ErrorMessage = "O campo DDD é obrigatório.")]
    [RegularExpression(@"^\d{2}$", ErrorMessage = "O DDD deve conter exatamente 2 dígitos numéricos.")]
    public int DDD { get; set; }

    [JsonPropertyName("phone_number")]
    [Required(ErrorMessage = "O campo telefone é obrigatório.")]
    [RegularExpression(@"^\d{1,9}$", ErrorMessage = "O telefone deve conter até 9 dígitos.")]
    public string PhoneNumber { get; set; }

    public BasicUser() : base() { }
}
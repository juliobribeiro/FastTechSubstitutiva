using FastTechKitchen.Domain.Enums;

namespace FastTechKitchen.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public PermissionLevel PermissionLevel { get; set; }
    public int DDD { get; set; }
    public string PhoneNumber { get; set; }

    public User() : base() { }
}
namespace FastTech.Domain.Enums;

[Flags]
public enum PermissionLevel
{
    Gerente = 1,
    Funcionario = 2,
    Cliente = 4,
}
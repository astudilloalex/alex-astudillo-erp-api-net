namespace AlexAstudilloERP.API.DTOs.Requests;

public class RoleRequestDTO
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public HashSet<short> PermissionIds { get; set; } = new HashSet<short>();
}

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Menu
{
    public short Id { get; set; }

    public short? ParentId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public short Order { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// Use Font Awesome Icons name on the latest version
    /// </summary>
    public string? Icon { get; set; }

    public string Path { get; set; } = null!;

    public bool IsPublic { get; set; }

    public virtual ICollection<Menu> InverseParent { get; set; } = new List<Menu>();

    public virtual Menu? Parent { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
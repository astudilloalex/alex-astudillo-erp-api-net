﻿using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Permission
{
    public short Id { get; set; }

    public string Code { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    [JsonIgnore]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}

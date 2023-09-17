using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Employee
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int CompanyId { get; set; }

    public int JobId { get; set; }

    public string Code { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public DateTime? Birthdate { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UserCode { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Job Job { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<JobHistory> JobHistories { get; set; } = new List<JobHistory>();

    public virtual Person Person { get; set; } = null!;
}

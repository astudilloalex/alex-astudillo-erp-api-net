namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class JobHistory
{
    public int EmployeeId { get; set; }

    public DateTime StartDate { get; set; }

    public int JobId { get; set; }

    public int DepartmentId { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Job Job { get; set; } = null!;
}

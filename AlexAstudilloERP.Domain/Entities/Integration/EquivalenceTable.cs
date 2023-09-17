using AlexAstudilloERP.Domain.Entities.Common;

namespace AlexAstudilloERP.Domain.Entities.Integration;

/// <summary>
/// Save equivalence data for integrations
/// </summary>
public partial class EquivalenceTable
{
    public string LocalValue { get; set; } = null!;

    public short OrganizationId { get; set; }

    public short MicroserviceId { get; set; }

    public short TableId { get; set; }

    public short DataTypeId { get; set; }

    public short LocalDataTypeId { get; set; }

    public string MicroserviceValue { get; set; } = null!;

    public virtual DataType DataType { get; set; } = null!;

    public virtual DataType LocalDataType { get; set; } = null!;

    public virtual Microservice Microservice { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}

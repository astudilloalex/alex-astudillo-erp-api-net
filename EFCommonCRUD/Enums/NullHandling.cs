namespace EFCommonCRUD.Enums;

/// <summary>
/// Enumeration for null handling hints that can be used in <see cref="Order"/> expressions.
/// </summary>
public enum NullHandling
{
    /// <summary>
    /// Lets the data store decide what to do with nulls.
    /// </summary>
    Native,

    /// <summary>
    /// A hint to the used data store to order entries with null values before non null entries.
    /// </summary>
    NullFirst,

    /// <summary>
    /// A hint to the used data store to order entries with null values after non null entries.
    /// </summary>
    NullLast,
}

using EFCommonCRUD.Enums;

namespace EFCommonCRUD.Models;

/// <summary>
/// Implements the pairing of an <see cref="Direction"/> and a property. It is used to provide input for
/// </summary>
public class Order
{
    private static readonly bool _defaultIgnoreCase = false;
    private static readonly NullHandling _defaultNullHandling = NullHandling.Native;

    private readonly Direction? _direction;
    private readonly string _property;
    private readonly bool _ignoreCase;
    private readonly NullHandling _nullHandling;

    /// <summary>
    /// Creates a new <see cref="Order"/> instance. if order is <c>null</c> then order defaults to <see cref="Sort.defaultDirection"/>
    /// </summary>
    /// <param name="direction">Can be <c>null</c>.</param>
    /// <param name="property">Must not be <c>null</c> or empty.</param>
    /// <param name="ignoreCase">True if sorting should be case-insensitive. false if sorting should be case-sensitive.</param>
    /// <param name="nullHandling">Must not be <c>null</c>.</param>
    /// <exception cref="ArgumentException">When property is <c>null</c> or empty.</exception>
    private Order(Direction? direction, string property, bool ignoreCase, NullHandling nullHandling)
    {
        if (string.IsNullOrEmpty(property.Trim())) throw new ArgumentException("Property must not be null or empty");
        _direction = direction == null ? Sort.defaultDirection : direction;
        _property = property;
        _ignoreCase = ignoreCase;
        _nullHandling = nullHandling;
    }

    /// <summary>
    /// Creates a new <see cref="Order"/> instance. If order is <c>null</c> then order defaults to <see cref="Sort.defaultDirection"/>
    /// </summary>
    /// <param name="direction">Can be <c>null</c>, will default to <see cref="Sort.defaultDirection"/>.</param>
    /// <param name="property">Must not be <c>null</c> or empty.</param>
    public Order(Direction? direction, string property) : this(direction, property, _defaultIgnoreCase, _defaultNullHandling)
    {

    }

    /// <summary>
    /// Creates a new <see cref="Order"/> instance. If order is <c>null</c> then order defaults to <see cref="Sort.defaultDirection"/>
    /// </summary>
    /// <param name="direction">Can be <c>null</c>, will default to <see cref="Sort.defaultDirection"/>.</param>
    /// <param name="property">Must not be <c>null</c> or empty.</param>
    /// <param name="nullHandling">Must not be <c>null</c>.</param>
    public Order(Direction? direction, string property, NullHandling nullHandling) : this(direction, property, _defaultIgnoreCase, nullHandling)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Order"/> instance. Takes a single property. Direction is <see cref="Direction.Asc"/> and
    /// NullHandling <see cref="NullHandling.Native"/>
    /// </summary>
    /// <param name="property">Must not be <c>null</c> or empty.</param>
    /// <returns>A new <see cref="Order"/></returns>
    public static Order Asc(string property)
    {
        return new Order(Direction.Asc, property, _defaultNullHandling);
    }

    /// <summary>
    /// Creates a new <see cref="Order"/> instance. Takes a single property. Direction defaults to <see cref="Sort.defaultDirection"/>
    /// </summary>
    /// <param name="property">Must not be <c>null</c> or empty.</param>
    /// <returns>A new <see cref="Order"/></returns>
    public static Order By(string property)
    {
        return new Order(Sort.defaultDirection, property);
    }

    /// <summary>
    /// Creates a new <see cref="Order"/> instance. Takes a single property. Direction is <see cref="Direction.Des"/> and
    /// NullHandling <see cref="NullHandling.Native"/>
    /// </summary>
    /// <param name="property">Must not be <c>null</c> or empty.</param>
    /// <returns>A new <see cref="Order"/></returns>
    public static Order Desc(string property)
    {
        return new Order(Direction.Desc, property, _defaultNullHandling);
    }

    /// <summary>
    /// Returns the order the property shall be sorted for.
    /// </summary>
    /// <returns>The <see cref="Direction"/></returns>
    public Direction GetDirection()
    {
        return _direction ?? Sort.defaultDirection;
    }

    /// <summary>
    /// Returns the property to order for.
    /// </summary>
    /// <returns>The property.</returns>
    public string GetProperty()
    {
        return _property;
    }

    /// <summary>
    /// Returns a new <see cref="Order"/> with case insensitive sorting enabled.
    /// </summary>
    /// <returns>A new <see cref="Order"/>.</returns>
    public Order IgnoreCase()
    {
        return new Order(_direction, _property, true, _nullHandling);
    }

    /// <summary>
    /// Returns whether sorting for this property shall be ascending.
    /// </summary>
    /// <returns>If ascending.</returns>
    public bool IsAscending()
    {
        return _direction == Direction.Asc;
    }

    /// <summary>
    /// Returns whether sorting for this property shall be descending.
    /// </summary>
    /// <returns>If descending.</returns>
    public bool IsDescending()
    {
        return _direction == Direction.Desc;
    }

    /// <summary>
    /// Returns whether the sort will be case-sensitive or case-insensitive.
    /// </summary>
    /// <returns>If case-insensitive.</returns>
    public bool IsIgnoreCase()
    {
        return _ignoreCase;
    }

    /// <summary>
    /// Returns a new <see cref="Order"/> with the given <see cref="Direction"/>.
    /// </summary>
    /// <param name="direction">The <see cref="Direction"/>.</param>
    /// <returns>A new <see cref="Order"/>.</returns>
    public Order With(Direction direction)
    {
        return new Order(direction, _property, _ignoreCase, _nullHandling);
    }

    /// <summary>
    /// Returns a <see cref="Order"/> with the given <see cref="NullHandling"/>.
    /// </summary>
    /// <param name="nullHandling">Must not be <c>null</c>.</param>
    /// <returns>A new <see cref="Order"/>.</returns>
    public Order With(NullHandling nullHandling)
    {
        return new Order(_direction, _property, _ignoreCase, nullHandling);
    }

    /// <summary>
    /// Returns a new <see cref="Order"/>.
    /// </summary>
    /// <param name="property">Must not be <c>null</c> or empty.</param>
    /// <returns>A new <see cref="Order"/>.</returns>
    public Order WithProperty(string property)
    {
        return new Order(_direction, property, _ignoreCase, _nullHandling);
    }
}

using EFCommonCRUD.Enums;

namespace EFCommonCRUD.Models;

/// <summary>
/// Sort option for queries. You have to provide at least a list of properties to sort for that must not include.
/// </summary>
public class Sort
{
    // Default unsorted.
    private static readonly Sort _unsorted = By(Array.Empty<Order>());
    // The orders of sort.
    private readonly List<Order> _orders;

    /// <summary>
    /// The default <see cref="Direction"/> of this sort.
    /// </summary>
    public static readonly Direction defaultDirection = Direction.Asc;

    /// <summary>
    /// Creates a new <see cref="Sort"/> instance.
    /// </summary>
    /// <param name="direction">The <see cref="Direction"/></param>
    /// <param name="properties">Must not be <c>null</c> or contain <c>null</c> or empty strings.</param>
    /// <exception cref="ArgumentException">When <paramref name="properties"/> is null or empty.</exception>
    private Sort(Direction direction, List<string> properties)
    {
        if (properties == null || properties.Count < 1)
        {
            throw new ArgumentException("You have to provide at least one property to sort by");
        }
        _orders = properties.Select(property => new Order(direction, property)).ToList();
    }

    /// <summary>
    /// Creates a new <see cref="Sort"/> instance.
    /// </summary>
    /// <param name="orders">A <see cref="Order"/> List.</param>
    protected Sort(List<Order> orders)
    {
        _orders = orders;
    }

    /// <summary>
    /// Returns a new <see cref="Sort"/> consisting of the <see cref="Order"/>s of the current {@link Sort} combined with the given ones
    /// </summary>
    /// <param name="sort">Must not be null.</param>
    /// <returns>A new <see cref="Sort"/>.</returns>
    public static Sort And(Sort sort)
    {
        List<Order> orders = [.. sort.GetOrders()];
        return By(orders);
    }

    /// <summary>
    /// Returns a new <see cref="Sort"/> with the current setup but ascending order direction.
    /// </summary>
    /// <returns>A new <see cref="Sort"/>.</returns>
    public Sort Ascending()
    {
        return WithDirection(Direction.Asc);
    }

    /// <summary>
    /// Create a new <see cref="Order"/> with direction and properties.
    /// </summary>
    /// <param name="direction">The <see cref="Direction"/>.</param>
    /// <param name="properties">The properties must not be null.</param>
    /// <returns>A new <see cref="Sort"/>.</returns>
    public static Sort By(Direction direction, string[] properties)
    {
        return By(properties.Select(property => new Order(direction, property)).ToList());
    }

    /// <summary>
    /// Creates a new <see cref="Sort"/> for the given <see cref="Order"/>s.
    /// </summary>
    /// <param name="orders">Must not be <c>null</c>.</param>
    /// <returns>A new <see cref="Sort"/></returns>
    public static Sort By(Order[] orders)
    {
        return new Sort([.. orders]);
    }

    /// <summary>
    /// Creates a new <see cref="Sort"/> for the given <see cref="Order"/>s.
    /// </summary>
    /// <param name="orders">Must not be <c>null</c>.</param>
    /// <returns>A new <see cref="Sort"/></returns>
    public static Sort By(List<Order> orders)
    {
        return orders.Count < 1 ? Unsorted() : new Sort(orders);
    }

    /// <summary>
    /// Creates a new <see cref="Sort"/> for the given properties.
    /// </summary>
    /// <param name="properties">Must not be <c>null</c>.</param>
    /// <returns>A new <see cref="Sort"/></returns>
    public static Sort By(string[] properties)
    {
        return properties.Length == 0 ? Unsorted() : new Sort(defaultDirection, [.. properties]);
    }

    /// <summary>
    /// Returns a new <see cref="Sort"/> with the current setup but descending order direction.
    /// </summary>
    /// <returns>A new <see cref="Sort"/>.</returns>
    public Sort Descending()
    {
        return WithDirection(Direction.Desc);
    }

    /// <summary>
    /// Returns all orders in this <see cref="Sort"/>
    /// </summary>
    /// <returns>A <see cref="List{Order}"/>.</returns>
    public List<Order> GetOrders()
    {
        return _orders;
    }

    /// <summary>
    /// Returns whether orders is empty.
    /// </summary>
    /// <returns>If orders is empty.</returns>
    public bool IsEmpty()
    {
        return _orders.Count < 1;
    }

    /// <summary>
    /// Returns if sorted.
    /// </summary>
    /// <returns>If sorted, otherwise false.</returns>
    public bool IsSorted()
    {
        return !IsEmpty();
    }

    /// <summary>
    /// Returns a <see cref="Sort"/> instances representing no sorting setup at all.
    /// </summary>
    /// <returns>A new <see cref="Sort"/></returns>
    public static Sort Unsorted()
    {
        return _unsorted;
    }

    /// <summary>
    /// Creates a new <see cref="Sort"/> with the current setup but the given order direction.
    /// </summary>
    /// <param name="direction">The <see cref="Direction"/></param>
    /// <returns>A new <see cref="Sort"/>.</returns>
    private Sort WithDirection(Direction direction)
    {
        return By(_orders.Select(order => order.With(direction)).ToList());
    }
}

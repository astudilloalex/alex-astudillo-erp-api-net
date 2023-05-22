namespace EFCommonCRUD.Models;

/// <summary>
/// A container object which may or may not contain a non-<c>null</c> value.
/// </summary>
/// <typeparam name="T">The type of value.</typeparam>
public sealed class Optional<T>
{
    // The value can be null.
    private static T? _value;
    // Contains a empty data.
    private static readonly Optional<T?> _empty = new(default);

    /// <summary>
    /// Constructs an instance with the described value.
    /// </summary>
    /// <param name="value">
    /// The value to describe; it's the caller's responsibility to
    /// ensure the value is non-<c>null</c> unless creating the singleton
    /// instance returned by <see cref="Empty{T}"/>.
    /// </param>
    private Optional(T? value)
    {
        _value = value;
    }

    /// <summary>
    /// Returns an empty <see cref="Optional{T}"/> instance.  No value is present for this <c>Optional</c>.
    /// </summary>
    /// <returns>An empty <c>Optional</c></returns>
    public static Optional<T?> Empty()
    {
        return _empty;
    }

    /// <summary>
    /// If a value is present, and the value matches the given predicate,
    /// returns an <see cref="Optional{T}"/> describing the value, otherwise returns an
    /// empty <see cref="Optional{T}"/>
    /// </summary>
    /// <param name="predicate">The predicate to apply to a value, if present.</param>
    /// <returns>The <c>Optional</c> value.</cc></returns>
    /// <exception cref="ArgumentNullException"> When predicate is null.</exception>
    public Optional<T?> Filter(Predicate<T> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        if (!IsPresent()) return this;
        return predicate(_value!) ? this : Empty();
    }

    /// <summary>
    /// If a value is present, returns the value, otherwise throws <c>InvalidOperationException</c>.
    /// </summary>
    /// <returns>The non-<c>null</c> value described by this <see cref="Optional{T}"/></returns>
    /// <exception cref="InvalidOperationException">If no value is present.</exception>
    public T Get()
    {
        if (_value == null) throw new InvalidOperationException("No value present");
        return _value;
    }

    /// <summary>
    /// If a value is  not present, returns <c>true</c>, otherwise <c>false</c>.
    /// </summary>
    /// <returns>If a value is not present.</returns>
    public bool IsEmpty()
    {
        return _value == null;
    }

    /// <summary>
    /// If a value is present, returns <c>true</c>, otherwise <c>false</c>.
    /// </summary>
    /// <returns>If values is present.</returns>
    public bool IsPresent()
    {
        return _value != null;
    }

    /// <summary>
    /// If a value is present, returns an <see cref="Optional{U?}"/> describing (as if by
    /// <see cref="OfNullable(T?)"/>) the result of applying the given mapping function to
    /// the value, otherwise returns an empty <see cref="Optional{U?}"/>
    /// </summary>
    /// <typeparam name="U">The type of the value returned from the mapping function</typeparam>
    /// <param name="mapper">The mapping function to apply to a value, if present.</param>
    /// <returns>An <see cref="Optional{U?}"/>.</returns>
    /// <exception cref="ArgumentNullException">If the mapping function is <c>null</c>.</exception>
    public Optional<U?> Map<U>(Func<T, U> mapper)
    {
        if (mapper == null) throw new ArgumentNullException(nameof(mapper));
        if (!IsPresent()) return Optional<U>.Empty();
        return Optional<U>.OfNullable(mapper(_value!));
    }

    /// <summary>
    /// Returns an <see cref="Optional{T}"/> describing the given non-<c>null</c> value.
    /// </summary>
    /// <param name="value">The value to describe, which must be non-<c>null</c></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">When <paramref name="value"/> is null.</exception>
    public static Optional<T> Of(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        return new Optional<T>(value);
    }

    /// <summary>
    /// Returns an <see cref="Optional{T}"/> describing the given value, if
    /// non-<c>null</c>, otherwise returns an empty <see cref="Optional{T}"/>.
    /// </summary>
    /// <param name="value">The possibly-<c>null</c> value to describe.</param>
    /// <returns>A <c>Optional</c></returns>
    public static Optional<T?> OfNullable(T? value)
    {
        return value == null ? _empty : new Optional<T?>(value);
    }

    /// <summary>
    /// If a value is present, returns the value, otherwise returns <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The other value to be returned if no value is present.</param>
    /// <returns></returns>
    public T? OrElse(T? other)
    {
        return _value != null ? _value : other;
    }
}

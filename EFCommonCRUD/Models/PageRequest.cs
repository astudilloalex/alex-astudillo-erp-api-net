using EFCommonCRUD.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace EFCommonCRUD.Models;

/// <summary>
/// Basic implementation of <see cref="IPageable"/>.
/// </summary>
public class PageRequest : AbstractPageRequest
{
    private readonly Sort _sort;

    /// <summary>
    /// Creates a new <see cref="PageRequest"/> with sort parameters applied.
    /// </summary>
    /// <param name="page">Zero-based page index, must not be negative.</param>
    /// <param name="size">The size of the page to be returned, must be greater than 0.</param>
    /// <param name="sort">Must not be <c>null</c>, use <see cref="Sort.Unsorted()"/> instead.</param>
    /// <exception cref="ArgumentNullException">When sort is <c>null</c>.</exception>
    protected PageRequest(int page, int size, Sort sort) : base(page, size)
    {
        _sort = sort ?? throw new ArgumentNullException(nameof(sort), "Sort must not be null");
    }

    /// <summary>
    /// Creates a new unsorted <see cref="PageRequest"/>.
    /// </summary>
    /// <param name="page">zero-based page index, must not be negative.</param>
    /// <param name="size">the size of the page to be returned, must be greater than 0.</param>
    /// <returns>A new <see cref="PageRequest"/>.</returns>
    public static PageRequest Of(int page, int size)
    {
        return Of(page, size, Sort.Unsorted());
    }

    /// <summary>
    /// Creates a new <see cref="PageRequest"/> with sort parameters applied.
    /// </summary>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">The size of the page to be returned.</param>
    /// <param name="sort">Must not be <c>null</c>, use <see cref="Sort.Unsorted()"/> instead.</param>
    /// <returns>A new <see cref="PageRequest"/>.</returns>
    public static PageRequest Of(int page, int size, Sort sort)
    {
        return new PageRequest(page, size, sort);
    }

    /// <summary>
    /// Creates a new <see cref="PageRequest"/> with sort direction and properties applied.
    /// </summary>
    /// <param name="page">Zero-based page index, must not be negative.</param>
    /// <param name="size">The size of the page to be returned, must be greater than 0.</param>
    /// <param name="direction">Must not be <c>null</c>.</param>
    /// <param name="properties">Must not be <c>null</c>.</param>
    /// <returns>A new <see cref="PageRequest"/>.</returns>
    public static PageRequest Of(int page, int size, Direction direction, string[] properties)
    {
        return Of(page, size, Sort.By(direction, properties));
    }

    /// <summary>
    /// Creates a new <see cref="PageRequest"/> for the first page (page number <c>0</c>) given <paramref name="pageSize"/> .
    /// </summary>
    /// <param name="pageSize">The size of the page to be returned, must be greater than 0.</param>
    /// <returns>A new <see cref="PageRequest"/>.</returns>
    public static PageRequest OfSize(int pageSize)
    {
        return Of(0, pageSize);
    }

    public override PageRequest First()
    {
        return new PageRequest(0, GetPageSize(), GetSort());
    }

    public override Sort GetSort()
    {
        return _sort;
    }

    public override PageRequest Next()
    {
        return new PageRequest(GetPageNumber() + 1, GetPageSize(), GetSort());
    }

    public override PageRequest Previous()
    {
        return GetPageNumber() == 0 ? this : new PageRequest(GetPageNumber() - 1, GetPageSize(), GetSort());
    }

    public override PageRequest WithPage(int pageNumber)
    {
        return new PageRequest(pageNumber, GetPageSize(), GetSort());
    }

    /// <summary>
    /// Creates a new <see cref="PageRequest"/> with <see cref="Sort"/> applied.
    /// </summary>
    /// <param name="sort">Must not be null.</param>
    /// <returns>A new <see cref="PageRequest"/>.</returns>
    public PageRequest WithSort(Sort sort)
    {
        return new PageRequest(GetPageNumber(), GetPageSize(), sort);
    }

    /// <summary>
    /// Creates a new <see cref="PageRequest"/> with <see cref="Direction"/> and <paramref name="properties"/> applied.
    /// </summary>
    /// <param name="direction">Must not be <c>null</c></param>
    /// <param name="properties">Must not be <c>null</c></param>
    /// <returns>A new <see cref="PageRequest"/>.</returns>
    public PageRequest WithSort(Direction direction, string[] properties)
    {
        return new PageRequest(GetPageNumber(), GetPageSize(), Sort.By(direction, properties));
    }

    public override int GetHashCode()
    {
        return 31 * base.GetHashCode() + _sort.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("Page request [number: {0}, size: {1}, sort: {2}]", GetPageNumber(), GetPageSize(), _sort);
    }

    public override bool Equals(object? obj)
    {
        if (this == obj) return true;
        if (obj?.GetType() != GetType()) return false;
        return base.Equals(obj) && _sort.Equals(((PageRequest)obj)._sort);
    }
}

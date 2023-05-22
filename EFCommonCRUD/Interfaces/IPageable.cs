using EFCommonCRUD.Models;
using System.Drawing;

namespace EFCommonCRUD.Interfaces;

/// <summary>
/// Abstract interface for pagination information.
/// </summary>
public interface IPageable
{
    /// <summary>
    /// Returns the <c>IPageable</c> requesting the first page.
    /// </summary>
    /// <returns>The first <c>IPageable</c></returns>
    public IPageable First();

    /// <summary>
    /// Returns the offset to be taken according to the underlying page and page size.
    /// </summary>
    /// <returns>The offset to be taken.</returns>
    public long GetOffset();

    /// <summary>
    /// Returns the page to be returned.
    /// </summary>
    /// <returns>The page to be returned.</returns>
    public int GetPageNumber();

    /// <summary>
    /// Returns the number of items to be returned.
    /// </summary>
    /// <returns>The number of items of that page.</returns>
    public int GetPageSize();

    /// <summary>
    /// Returns the sorting parameters.
    /// </summary>
    /// <returns>Sorting parameters.</returns>
    public Sort GetSort();

    /// <summary>
    /// Returns the current <see cref="Sort"/> or the given one if the current one is unsorted.
    /// </summary>
    /// <param name="sort">Must not be <c>null</c>.</param>
    /// <returns>A <see cref="Sort"/>.</returns>
    public Sort GetSortOr(Sort sort)
    {
        return GetSort().IsSorted() ? GetSort() : sort;
    }

    /// <summary>
    /// Returns whether there's a previous <c>IPageable</c> we can access from the current one.
    /// </summary>
    /// <returns>In case the current <c>IPageable</c> already refers to the first page.</returns>
    public bool HasPrevious();

    /// <summary>
    /// Returns whether the current <c>IPageable</c> contains pagination information.
    /// </summary>
    /// <returns>If contain information.</returns>
    public bool IsPaged()
    {
        return true;
    }

    /// <summary>
    /// Returns whether the current <c>IPageable</c> does not contain pagination information.
    /// </summary>
    /// <returns>If does not contain information.</returns>
    public bool IsUnpaged()
    {
        return !IsPaged();
    }

    /// <summary>
    /// Returns the <c>IPageable</c> requesting the next <c>IPage</c>.
    /// </summary>
    /// <returns>The next page.</returns>
    public IPageable Next();

    /// <summary>
    /// Creates a new <c>IPageable</c> for the first page (page number <c>0</c>) given <c>pageSize</c>.
    /// </summary>
    /// <param name="pageSize">The size of the page to be returned, must be greater than 0.</param>
    /// <returns>A new <c>IPageable</c>.</returns>
    public static IPageable OfSize(int pageSize)
    {
        return PageRequest.Of(0, pageSize);
    }

    /// <summary>
    /// Returns the previous <c>IPageable</c> or the first <c>IPageable</c> if the current one already is the first one.
    /// </summary>
    /// <returns>The previous or first <c>IPageable</c>.</returns>
    public IPageable PreviousOrFirst();

    /// <summary>
    /// Returns an <see cref="Optional{IPageable}"/> so that it can easily be mapped on.
    /// </summary>
    /// <returns>A <c>Optional</c>.</returns>
    public Optional<IPageable?> ToOptional()
    {
        return IsUnpaged() ? Optional<IPageable?>.Empty() : Optional<IPageable?>.Of(this);
    }

    /// <summary>
    /// Returns a <c>IPageable</c> instance representing no pagination setup.
    /// </summary>
    /// <returns>The <c>IPageable</c>.</returns>
    public static IPageable Unpaged()
    {
        return new Unpaged();
    }

    /// <summary>
    /// Creates a new <c>IPageblae</c> with <c>pageNumber</c> applied.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <returns>A new <c>IPageable</c>.</returns>
    public IPageable WithPage(int pageNumber);
}

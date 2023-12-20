using EFCommonCRUD.Interfaces;

namespace EFCommonCRUD.Models;

/// <summary>
/// Abstract implementation of <see cref="IPageable"/>.
/// </summary>
public abstract class AbstractPageRequest : IPageable, IEquatable<AbstractPageRequest>
{
    private readonly int _page;
    private readonly int _size;

    /// <summary>
    /// Creates a new <c>AbstractPageRequest</c>. Pages are zero indexed, thus providing 0 for <c>page</c> will return
    /// the first page.
    /// </summary>
    /// <param name="page">Must not be less than zero.</param>
    /// <param name="size">Must not be less than one.</param>
    /// <exception cref="ArgumentException">When page is less than zero or size is less than one.</exception>
    public AbstractPageRequest(int page, int size)
    {
        if (page < 0) throw new ArgumentException("Page index must not be less than zero");
        if (size < 1) throw new ArgumentException("Page size must not be less than one");
        _page = page;
        _size = size;
    }

    public abstract IPageable First();

    public long GetOffset()
    {
        return _page * _size;
    }

    public int GetPageNumber()
    {
        return _page;
    }

    public int GetPageSize()
    {
        return _size;
    }

    public abstract Sort GetSort();

    public bool HasPrevious()
    {
        return _page > 0;
    }

    public abstract IPageable Next();

    /// <summary>
    /// Returns the <see cref="IPageable"/> requesting the previous <see cref="IPage{T}"/>.
    /// </summary>
    /// <returns></returns>
    public abstract IPageable Previous();

    public IPageable PreviousOrFirst()
    {
        return HasPrevious() ? Previous() : First();
    }

    public override int GetHashCode()
    {
        int prime = 31;
        int result = 1;

        result = prime * result + _page;
        result = prime * result + _size;

        return result;
    }

    public bool Equals(AbstractPageRequest? other)
    {
        if (this == other) return true;
        if (other == null) return false;
        return _page == other._page && _size == other._size;
    }

    public abstract IPageable WithPage(int pageNumber);

    public override bool Equals(object? obj) => Equals(obj as AbstractPageRequest);
}

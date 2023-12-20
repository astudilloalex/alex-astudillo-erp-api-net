using EFCommonCRUD.Interfaces;
using System.Collections.ObjectModel;

namespace EFCommonCRUD.Models;

/// <summary>
/// A chunk of data restricted by the configured <see cref="IPageable"/>.
/// </summary>
/// <typeparam name="T">A class type.</typeparam>
public abstract class Chunk<T> : ISlice<T>
{
    private readonly List<T> _content = [];
    private readonly IPageable _pageable;

    /// <summary>
    /// Creates a new <see cref="Chunk{T}"/> with the given content and the given governing <see cref="IPageable"/>.
    /// </summary>
    /// <param name="content">Must not be null.</param>
    /// <param name="pageable">Must not be null.</param>
    public Chunk(List<T> content, IPageable pageable)
    {
        _content.AddRange(content);
        _pageable = pageable;
    }

    public List<T> GetContent()
    {
        return new ReadOnlyCollection<T>(_content).ToList();
    }

    public int GetNumber()
    {
        return _pageable.IsPaged() ? _pageable.GetPageNumber() : 0;
    }

    public int GetNumberOfElements()
    {
        return _content.Count;
    }

    public IPageable GetPageable()
    {
        return _pageable;
    }

    public int GetSize()
    {
        return _pageable.IsPaged() ? _pageable.GetPageSize() : _content.Count;
    }

    public Sort GetSort()
    {
        return _pageable.GetSort();
    }

    public bool HasContent()
    {
        return _content.Count > 0;
    }

    public bool HasNext()
    {
        throw new NotImplementedException();
    }

    public bool HasPrevious()
    {
        return GetNumber() > 0;
    }

    public bool IsFirst()
    {
        return !HasPrevious();
    }

    public bool IsLast()
    {
        return !HasNext();
    }

    public ISlice<U> Map<U>(Func<T, U> converter)
    {
        throw new NotImplementedException();
    }

    public IPageable NextPageable()
    {
        return HasNext() ? _pageable.Next() : IPageable.Unpaged();
    }

    public IPageable PreviousOrFirstPageable()
    {
        throw new NotImplementedException();
    }

    public IPageable PreviousPageable()
    {
        return HasPrevious() ? _pageable.PreviousOrFirst() : IPageable.Unpaged();
    }
}

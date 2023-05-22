using EFCommonCRUD.Models;

namespace EFCommonCRUD.Interfaces;

/// <summary>
/// A slice of data that indicates whether there's a next or previous slice available. Allows to obtain a
/// <see cref="IPageable"/> to request a previous or next <see cref="ISlice{T}"/>.
/// </summary>
public interface ISlice<T>
{
    /// <summary>
    /// Returns the page content as <see cref="List{T}"/>.
    /// </summary>
    /// <returns>A <see cref="List{T}"/></returns>
    public List<T> GetContent();

    /// <summary>
    /// Returns the number of the current <see cref="ISlice{T}"/>. Is always non-negative.
    /// </summary>
    /// <returns>The number of the current <see cref="ISlice{T}"/></returns>
    public int GetNumber();

    /// <summary>
    /// Returns the number of elements currently on this <see cref="ISlice{T}"/>.
    /// </summary>
    /// <returns></returns>
    public int GetNumberOfElements();

    /// <summary>
    /// Returns the <see cref="IPageable"/> that's been used to request the current <see cref="ISlice{T}"/>.
    /// </summary>
    /// <returns>A IPageable.</returns>
    public IPageable GetPageable()
    {
        return PageRequest.Of(GetNumber(), GetSize(), GetSort());
    }

    /// <summary>
    /// Returns the size of the <see cref="ISlice{T}"/>.
    /// </summary>
    /// <returns>The size.</returns>
    public int GetSize();

    /// <summary>
    /// Returns the sorting parameters for the <see cref="ISlice{T}"/>.
    /// </summary>
    /// <returns>The sort.</returns>
    public Sort GetSort();

    /// <summary>
    /// Returns whether the <see cref="ISlice{T}"/> has content at all.
    /// </summary>
    /// <returns>If has content.</returns>
    public bool HasContent();

    /// <summary>
    /// Returns if there is a next <see cref="ISlice{T}"/>.
    /// </summary>
    /// <returns>If there is a next.</returns>
    public bool HasNext();

    /// <summary>
    /// Returns if there is a previous <see cref="ISlice{T}"/>.
    /// </summary>
    /// <returns>If there is a previous.</returns>
    public bool HasPrevious();

    /// <summary>
    /// Returns whether the current <see cref="ISlice{T}"/> is the first one.
    /// </summary>
    /// <returns>If first.</returns>
    public bool IsFirst();

    /// <summary>
    /// Returns whether the current <see cref="ISlice{T}"/> is the last one.
    /// </summary>
    /// <returns>If last.</returns>
    public bool IsLast();

    /// <summary>
    /// Returns a new <see cref="ISlice{T}"/> with the content of the current one mapped by the given converter.
    /// </summary>
    /// <typeparam name="U">A type of <see cref="ISlice{U}"/></typeparam>
    /// <param name="converter">Function must not be null.</param>
    /// <returns>A new <see cref="ISlice{U}"/>.</returns>
    public ISlice<U> Map<U>(Func<T, U> converter);

    /// <summary>
    /// Returns the <see cref="IPageable"/> describing the next slice or the one describing the current slice in case it's the
    /// last one.
    /// </summary>
    /// <returns>The next or last <see cref="IPageable"/></returns>
    public IPageable NextOrLastPageable()
    {
        return HasNext() ? NextPageable() : GetPageable();
    }

    /// <summary>
    /// Returns the <see cref="IPageable"/> to request the next <see cref="ISlice{T}"/>. Can be <see cref="IPageable.Unpaged()"/> in case the
    /// current <see cref="ISlice{T}"/> is already the last one. Clients should check <see cref="HasNext()"/> before calling this method.
    /// </summary>
    /// <returns>A next IPageable.</returns>
    public IPageable NextPageable();

    /// <summary>
    /// Returns the <see cref="IPageable"/> describing the previous slice or the one describing the current slice in case it's the
    /// first one.
    /// </summary>
    /// <returns>The previous or first <see cref="IPageable"/></returns>
    public IPageable PreviousOrFirstPageable();

    /// <summary>
    /// Returns the <see cref="IPageable"/> to request the previous <see cref="ISlice{T}"/>. Can be <see cref="IPageable.Unpaged()"/> in case the
    /// current <see cref="ISlice{T}"/> is already the first one. Clients should check <see cref="HasPrevious()"/> before calling this method.
    /// </summary>
    /// <returns>A previous IPageable.</returns>
    public IPageable PreviousPageable();
}

using EFCommonCRUD.Interfaces;

namespace EFCommonCRUD.Models;

internal class Unpaged : IPageable
{
    public IPageable First()
    {
        return this;
    }

    public long GetOffset()
    {
        throw new NotImplementedException();
    }

    public int GetPageNumber()
    {
        throw new NotImplementedException();
    }

    public int GetPageSize()
    {
        throw new NotImplementedException();
    }

    public Sort GetSort()
    {
        return Sort.Unsorted();
    }

    public bool HasPrevious()
    {
        return false;
    }

    public bool IsPaged()
    {
        return false;
    }

    public IPageable Next()
    {
        return this;
    }

    public IPageable PreviousOrFirst()
    {
        return this;
    }

    public IPageable WithPage(int pageNumber)
    {
        if (pageNumber == 0) return this;
        throw new NotImplementedException();
    }
}

using EFCommonCRUD.Interfaces;
using System.Runtime.Serialization;

namespace EFCommonCRUD.Models
{
    /// <summary>
    /// Basic <see cref="IPage{T}"/> implementation.
    /// </summary>
    /// <typeparam name="T">The type of which the page consists.</typeparam>
    /// <remarks>
    /// Constructor of <see cref="Page{T}"/>.
    /// </remarks>
    /// <param name="content">The content of this page, must not be <c>null</c>.</param>
    /// <param name="pageable">The paging information, must not be <c>null</c>.</param>
    /// <param name="total">The total amount of items available.</param>
    public class Page<T>(List<T> content, IPageable pageable, long total) : Chunk<T>(content, pageable), IPage<T>, ISerializable where T : class
    {
        private readonly long _total = total;

        /// <summary>
        /// Creates a new <see cref="Page{T}"/> with the given content. This will result in the created <see cref="Page{T}"/> being identical
        /// to the entire <see cref="List{T}"/>
        /// </summary>
        /// <param name="content">Must not be <c>null</c>.</param>
        public Page(List<T> content) : this(content, IPageable.Unpaged(), content == null ? 0 : content.Count)
        {
        }

        public long GetTotalElements()
        {
            return _total;
        }

        public int GetTotalPages()
        {
            return GetSize() == 0 ? 1 : (int)Math.Ceiling((double)_total / GetSize());
        }

        public new bool HasNext()
        {
            return GetNumber() + 1 < GetTotalPages();
        }

        public new bool IsLast()
        {
            return !HasNext();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("totalElements", GetTotalElements());
            info.AddValue("totalPages", GetTotalPages());
            info.AddValue("numberOfElements", GetNumberOfElements());
            info.AddValue("pageNumber", GetNumber() + 1);
            info.AddValue("last", IsLast());
            info.AddValue("first", IsFirst());
            info.AddValue("offset", GetPageable().GetOffset() + 1);
            throw new NotImplementedException();
        }
    }
}

using System.Collections;

namespace Port.Driven.Shared.Persistence;

public sealed class Page<T> : IPage<T>
{
    public Page(IEnumerable<T> content, long totalElements, IPageable pageable)
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        TotalElements = totalElements switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(totalElements)),
            _ => totalElements,
        };
        PageNumber = pageable?.PageNumber ?? throw new ArgumentNullException(nameof(pageable));
        PageSize = pageable.PageSize;
        TotalPages = (int)Math.Ceiling((double)totalElements / PageSize);
    }

    public IEnumerable<T> Content { get; }
    public long TotalElements { get; }
    public int TotalPages { get; }
    public int PageNumber { get; }
    public int PageSize { get; }

    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
    public bool IsFirst => PageNumber == 1;
    public bool IsLast => PageNumber == TotalPages;

    public IPage<U> Map<U>(Func<T, U> converter)
    {
        ArgumentNullException.ThrowIfNull(converter);
        return new Page<U>(
            Content.Select(converter),
            TotalElements,
            PageRequest.Of(PageNumber, PageSize)
        );
    }

    public IEnumerator<T> GetEnumerator() => Content.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public interface IPage<out T> : IEnumerable<T>
{
    IEnumerable<T> Content { get; }
    long TotalElements { get; }
    int TotalPages { get; }
    int PageNumber { get; }
    int PageSize { get; }
    bool HasNextPage { get; }
    bool HasPreviousPage { get; }
    bool IsFirst { get; }
    bool IsLast { get; }

    IPage<U> Map<U>(Func<T, U> converter);
}

public interface IPageable
{
    int PageNumber { get; }
    int PageSize { get; }
    int Offset { get; }
}

public sealed class PageRequest : IPageable
{
    private PageRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
    public int Offset => (PageNumber - 1) * PageSize;

    public static PageRequest Of(int pageNumber, int pageSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);
        return new PageRequest(pageNumber, pageSize);
    }
}

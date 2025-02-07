namespace CodeUp.Common.Responses;
public class PagedResponse<TData> : Response<TData>
{
    private const int DEFAULT_PAGE_SIZE = 10;
    private const int DEFAULT_PAGE = 1;

    public PagedResponse() { }
    public PagedResponse(
        int totalCount,
        TData? data = default,
        int currentPage = DEFAULT_PAGE,
        int pageSize = DEFAULT_PAGE_SIZE,
        int code = DEFAULT_STATUS_CODE,
        string? message = null,
        List<string?>? errors = null)
        : base(data, code, message, errors)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public PagedResponse(
        TData? data,
        int code = DEFAULT_STATUS_CODE,
        string? message = null,
        List<string?>? errors = null)
        : base(data, code, message, errors)
    {

    }

    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
    public int TotalCount { get; set; }
}

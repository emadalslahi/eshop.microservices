namespace eshop.buildingblocks.Pagination;

public class PaginationResult<TEntity>
    (int pageIndex ,
    int pageSize , 
    long count , 
    IEnumerable<TEntity> data)
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long TotalCount { get; } = count;
    public IEnumerable<TEntity> Data { get; } = data;
    public int Count => Data?.Count() ?? 0;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    
    public PaginationResult() : this(1, 10, 0, new List<TEntity>())
    {
    }
}

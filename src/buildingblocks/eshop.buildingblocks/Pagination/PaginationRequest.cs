namespace eshop.buildingblocks.Pagination;

public class PaginationRequest
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public PaginationRequest(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
    public PaginationRequest() { }
    public bool IsValid => PageIndex > 0 && PageSize > 0;
}
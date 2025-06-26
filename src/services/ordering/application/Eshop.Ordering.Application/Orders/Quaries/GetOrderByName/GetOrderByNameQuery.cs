namespace Eshop.Ordering.Application.Orders.Quaries.GetOrderByName;

public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;
public record GetOrdersByNameResult : BaseResult
{
  public IEnumerable<OrderDto> Orders { get; set; }
    public GetOrdersByNameResult(IEnumerable<OrderDto> orders,
                                bool isSuccess = true,
                                string message="")
    {
        Orders = orders;
        IsSuccess = isSuccess;
        ErrorMessage = message;
    }

}

public record BaseResult
{
    public BaseResult()
    {
        
    }
    public bool IsSuccess { get; set; } = true;
    public string? ErrorMessage { get; set; }
}
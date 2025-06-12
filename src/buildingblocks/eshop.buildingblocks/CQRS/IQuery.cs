using MediatR;
namespace eshop.buildingblocks.CQRS;

public interface IQuery<out TResponse> :IRequest<TResponse>
    where TResponse : notnull
{
}

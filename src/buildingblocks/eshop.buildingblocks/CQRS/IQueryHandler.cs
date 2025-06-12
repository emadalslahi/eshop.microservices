using MediatR;

namespace eshop.buildingblocks.CQRS;


/// <summary>
///     Represents a query handler in the CQRS pattern. With Response Offcours : No Query With No Response!
/// </summary>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IQueryHandler<in TQuery ,TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}

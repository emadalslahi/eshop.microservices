
using MediatR;

namespace eshop.buildingblocks.CQRS;


/// <summary>
/// Represents a command handler in the CQRS pattern. With No Respons
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand ,Unit>
    where TCommand : ICommand<Unit>
{
}


/// <summary>
/// Represents a command handler in the CQRS pattern. With Response 
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<in TCommand ,TResponse> : IRequestHandler<TCommand, TResponse>
    where TResponse : notnull
    where TCommand : ICommand<TResponse>
{
}

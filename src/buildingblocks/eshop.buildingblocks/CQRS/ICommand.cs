

using MediatR;

namespace eshop.buildingblocks.CQRS;


/// <summary>
///  Represents a command in the CQRS pattern. 
///  return no Response : inhrt Icommand 
/// </summary>
public interface ICommand : ICommand<Unit> 
{

}
public interface ICommand<out TResponse>:IRequest<TResponse>
{
}

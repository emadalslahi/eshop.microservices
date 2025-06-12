
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics; 

namespace eshop.buildingblocks.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> 
    (ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
     where TRequest :notnull ,IRequest<TResponse>
     where TResponse :notnull
   
{
    public async Task<TResponse> Handle(TRequest request, 
                                  RequestHandlerDelegate<TResponse> next, 
                                  CancellationToken cancellationToken)
    {
        logger.LogInformation("[STRT] Handling Requst :[{request}] - Response:[{response}] - RequestData:[{requsetData}]",
                               typeof(TRequest).Name , typeof(TResponse).Name ,request);
        var timer = new Stopwatch();
        timer.Start();
        var response = await next();
        timer.Stop();
        if (timer.Elapsed.Seconds > 3)
            logger.LogWarning("[PRFRMNS] The request [{request}] took [{TimeTaken}] Sconds",
                typeof(TRequest).Name , timer.Elapsed.Seconds);
        logger.LogInformation("[END] Handled The request [{request}] with [{response}]",typeof(TRequest).Name,typeof(TResponse).Name);
        return response;

    }
}

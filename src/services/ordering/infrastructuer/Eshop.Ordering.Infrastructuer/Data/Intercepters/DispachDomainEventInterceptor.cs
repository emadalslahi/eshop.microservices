using Eshop.Ordering.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Eshop.Ordering.Infrastructuer.Data.Intercepters;

public class DispachDomainEventInterceptor(IMediator _mediator):SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
         DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavedChanges(eventData, result);
    }
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
       await DispatchDomainEvents(eventData.Context);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;
        
        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a => a.Entity);

        var dominEvents = aggregates
            .SelectMany(a => a.DomainEvents)
            .ToList();

        aggregates
            .ToList()
            .ForEach(a => a.ClearDomainEvents());

        foreach (var domainEvent in dominEvents)
        await _mediator.Publish(domainEvent);
    }
}

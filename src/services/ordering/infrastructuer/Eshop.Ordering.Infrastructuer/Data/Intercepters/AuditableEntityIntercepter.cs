using Eshop.Ordering.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Runtime.CompilerServices;

namespace Eshop.Ordering.Infrastructuer.Data.Intercepters;

public class AuditableEntityIntercepter:SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // Logic to handle auditable entities before saving changes
        // For example, setting CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, etc.
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        foreach(var entry in context.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = "System"; // Replace with actual user if available
                // Set CreatedBy if needed
            }
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntity())
            {
                entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                entry.Entity.LastUpdatedBy = "System"; // Replace with actual user if available
                entry.Entity.UpdatedCount = entry.Entity.UpdatedCount ==null ? 0 : entry.Entity.UpdatedCount+1;    
                // Set ModifiedBy if needed
            }
        }
    }
 
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


}
// Extension method to check if an entity has changed
public static class EntityEntryExtensions
{
    public static bool HasChangedOwnedEntity(this EntityEntry entry) =>
          entry.References.Any(r => r.TargetEntry != null &&
                                   r.TargetEntry.Metadata.IsOwned() &&
          (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));


}


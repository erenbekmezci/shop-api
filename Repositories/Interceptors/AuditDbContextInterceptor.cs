using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Repositories.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private void AddBehavior(DbContext context, IAuditEntity auditEntity)
        {
           
            auditEntity.Created = DateTime.Now;
            context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;         
        }
        private void UpdateBehavior(DbContext context, IAuditEntity auditEntity)
        {
            context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
            auditEntity.Updated = DateTime.Now;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                IAuditEntity auditEntity;
                if (entityEntry.Entity is not IAuditEntity)
                {
                    continue;
                }
                switch (entityEntry.State)
                {

                    case EntityState.Added:

                        auditEntity = (IAuditEntity)entityEntry.Entity;

                        AddBehavior(eventData.Context , auditEntity);

                        break;
                    case EntityState.Modified:
                        auditEntity = (IAuditEntity)entityEntry.Entity;

                        UpdateBehavior(eventData.Context, auditEntity);

                        break;
                    default:
                        break;
                }


            }


            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}

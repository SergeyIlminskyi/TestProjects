using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SWAG.Data
{
    public partial class AppDbContext
        : DbContext
    {
        public static String ConnectionString { get; private set; }

        public static Int32 Timeout { get; } = 180;

        public DbSet<EventEntity> Events { get; set; }

        public DbSet<OperationEntity> Operations { get; set; }

        static AppDbContext()
        {
            ConnectionString = Environment.GetEnvironmentVariable("SWAG_DB_CONNECTIONSTRING")?.Trim('"');
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            if (Database.IsSqlServer())
            {
                Database.SetCommandTimeout(Timeout);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventEntity>().ToTable(nameof(EventEntity).Replace(nameof(Entity), String.Empty));
            modelBuilder.Entity<OperationEntity>().ToTable(nameof(OperationEntity).Replace(nameof(Entity), String.Empty));

            #region Operation

            modelBuilder.Entity<OperationEntity>()
                .Ignore(o => o.Value);

            FillData(modelBuilder.Entity<OperationEntity>());

            #endregion
        }

        public override Int32 SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected virtual void AddTimestamps()
        {
            IEnumerable<EntityEntry> entities = ChangeTracker.Entries().Where(e =>
            {
                if (typeof(IEntity).IsAssignableFrom(e.Entity.GetType()) &&
                    e.State == EntityState.Added)
                {
                    return true;
                }

                if (typeof(IHistoryEntity).IsAssignableFrom(e.Entity.GetType()) &&
                    e.State == EntityState.Modified)
                {
                    return true;
                }

                return false;
            });

            DateTime now = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                if (typeof(IHistoryEntity).IsAssignableFrom(entity.Entity.GetType()))
                {
                    ((IHistoryEntity)entity.Entity).ModifiedOn = now;
                }

                if (typeof(IEntity).IsAssignableFrom(entity.Entity.GetType()) &&
                    entity.State == EntityState.Added)
                {
                    ((IEntity)entity.Entity).CreatedOn = now;
                }
            }
        }
    }
}

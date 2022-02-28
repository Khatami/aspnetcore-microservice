using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System.Reflection;

namespace Ordering.Infrastructure.Persistence
{
	public class OrderContext : DbContext
	{
		public OrderContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Order> Orders { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<EntityBase>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedDate = DateTime.Now;
						entry.Entity.CreatedBy = "swn";
						break;

					case EntityState.Modified:
						entry.Entity.LastModifiedDate = DateTime.Now;
						entry.Entity.LastModifiedBy = "swn";
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}
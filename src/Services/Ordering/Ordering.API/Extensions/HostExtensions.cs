using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions
{
	public static class HostExtensions
	{
		public static IHost MigrateDatabase<TContext>(this IHost host,
			Action<TContext, IServiceProvider> seeder,
			int? retry = 0) where TContext : DbContext
		{
			int retryForAvailability = retry.Value;

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var logger = services.GetRequiredService<ILogger<TContext>>();
				var context = services.GetRequiredService<TContext>();

				try
				{
					logger.LogInformation
						("Migrating database associated with context {dbContextName}", typeof(TContext).Name);

					context.Database.Migrate();
					seeder(context, services);

					logger.LogInformation
						("Migrated database associated with context {dbContextName}", typeof(TContext).Name);
				}
				catch (SqlException ex)
				{
					logger.LogError(ex, "an error occurred while migrating the postgresql database");

					if (retryForAvailability < 5)
					{
						retryForAvailability++;
						Thread.Sleep(2000);

						MigrateDatabase<TContext>(host, seeder, retryForAvailability);
					}
				}
			}

			return host;
		}
	}
}

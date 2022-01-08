using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Extensions
{
	public static class HostExtensions
	{
		public static IHost MigrateDatabase<TContext>(this IHost host, int retryAvailablility = 0)
		{
			using (var serviceScope = host.Services.CreateScope())
			{
				var serviceProvider = serviceScope.ServiceProvider;

				var configuration = serviceProvider.GetRequiredService<IConfiguration>();
			}

			return null;
		}
	}
}

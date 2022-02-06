using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
	internal class OrderContextSeed
	{
		internal static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
		{
			if (context.Orders.Any() == false)
			{
				await context.Orders.AddRangeAsync(new List<Order>
				{
					new Order()
					{
						UserName = "swn",
						FirstName = "Seyedhamed",
						LastName = "Khatami",
						EmailAddress = "hamedkhatami@outlook.com"
					}
				});

				logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext));

				await context.SaveChangesAsync();
			}
		}
	}
}

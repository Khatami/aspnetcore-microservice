using Npgsql;

namespace Discount.API.Extensions
{
	public static class HostExtensions
	{
		public static IHost MigrateDatabase<TContext>(this IHost host, int retryAvailablility = 0)
		{
			using (var serviceScope = host.Services.CreateScope())
			{
				var serviceProvider = serviceScope.ServiceProvider;

				var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
				using var connection = serviceProvider.GetRequiredService<NpgsqlConnection>();

				try
				{
					logger.LogInformation("Migrating postgresql database.");

					connection.Open();
					using var command = new NpgsqlCommand()
					{
						Connection = connection,
						CommandText = @"
							CREATE TABLE IF NOT EXISTS coupon
							(
								id SERIAL PRIMARY KEY NOT NULL,
								productname VARCHAR(24) NOT NULL,
								description Text,
								amount INT
							);

							INSERT INTO public.coupon (productname, description, amount)
							SELECT 'IPhone 10', 'Samsung Discount', 150
							WHERE NOT EXISTS (SELECT * FROM public.coupon WHERE productname = 'IPhone 10');

							INSERT INTO public.coupon (productname, description, amount)
							SELECT 'IPhone X', 'IPhone Discount', 200
							WHERE NOT EXISTS (SELECT * FROM public.coupon WHERE productname = 'IPhone X');
						"
					};

					command.ExecuteNonQuery();

					logger.LogInformation("Migrated postgresql database.");
				}
				catch (NpgsqlException ex)
				{
					logger.LogError(ex, "an error occurred while migrating the postgresql database");

					if (retryAvailablility < 5)
					{
						retryAvailablility++;
						Thread.Sleep(2000);

						MigrateDatabase<TContext>(host, retryAvailablility);
					}
				}
			}

			return host;
		}
	}
}

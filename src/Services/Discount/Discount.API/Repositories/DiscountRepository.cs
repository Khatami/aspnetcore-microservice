using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly IConfiguration _configuration;
		public DiscountRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<Coupon> GetDiscount(string productName)
		{
			string connectionString = _configuration.GetSection("DatabaseSettings")
				.GetValue<string>("ConnectionString");

			using var connection = new NpgsqlConnection(connectionString);

			var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
				("SELECT * FROM public.coupon WHERE productname = @productName", new { productName = productName });

			if (coupon == null)
			{
				return new Coupon()
				{
					ProductName = "No Discount",
					Amount = 0,
					Description = "No Discount Desc"
				};
			}

			return coupon;
		}

		public async Task<bool> CreateDiscount(Coupon coupon)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteDiscount(string productName)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateDiscount(Coupon coupon)
		{
			throw new NotImplementedException();
		}
	}
}
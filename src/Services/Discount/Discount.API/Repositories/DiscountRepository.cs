using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly IConfiguration _configuration;
		private readonly NpgsqlConnection _connection;
		public DiscountRepository(IConfiguration configuration, NpgsqlConnection connection)
		{
			_configuration = configuration;
			_connection = connection;
		}

		public async Task<Coupon> GetDiscount(string productName)
		{
			var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>
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
			var affected = await _connection.ExecuteAsync(@"
				INSERT INTO public.coupon (productname, description, amount)
				VALUES (@productName, @description, @amount);",
				new
				{
					productName = coupon.ProductName,
					description = coupon.Description,
					amount = coupon.Amount
				});

			if (affected == 0)
				return false;

			return true;
		}

		public async Task<bool> UpdateDiscount(Coupon coupon)
		{
			var affected = await _connection.ExecuteAsync(@"
				UPDATE public.coupon
				SET productname = @productName,
					description = @description,
					amount = @amount
				WHERE id = @ID",
				new
				{
					ID = coupon.ID,
					productname = coupon.ProductName,
					description = coupon.Description,
					amount = coupon.Amount
				});

			if (affected == 0)
				return false;

			return true;
		}

		public async Task<bool> DeleteDiscount(string productName)
		{
			var affected = await _connection.ExecuteAsync("DELETE FROM public.coupon WHERE productname = @productName", new
			{
				productname = productName
			});

			if (affected == 0)
				return false;

			return true;
		}
	}
}
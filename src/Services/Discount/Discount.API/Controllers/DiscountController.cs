using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiscountController : ControllerBase
	{
		private readonly IDiscountRepository _discountRepository;

		public DiscountController(IDiscountRepository discountRepository)
		{
			_discountRepository = discountRepository;
		}

		[HttpGet("{productName")]
		public async Task<ActionResult<Coupon>> GetDiscount(string productName)
		{
			var discount = await _discountRepository.GetDiscount(productName);

			return Ok(discount);
		}
	}
}
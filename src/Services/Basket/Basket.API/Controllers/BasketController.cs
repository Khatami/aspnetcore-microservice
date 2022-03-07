using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
		private readonly DiscountGrpcService _discountGrpcService;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository,
			DiscountGrpcService discountGrpcService,
			IMapper mapper)
		{
			_basketRepository = basketRepository;
			_discountGrpcService = discountGrpcService;
			_mapper = mapper;
		}

		[HttpGet("{userName}", Name = "GetBasket")]
		[ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
		public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
		{
			var basket = await _basketRepository.GetBasket(userName);

			return Ok(basket ?? new ShoppingCart(userName));
		}

		[HttpPost]
		[ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
		public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
		{
			foreach (var item in shoppingCart.Items)
			{
				var couponModel = await _discountGrpcService.GetDiscountAsync(item.ProductName);
				item.Price -= couponModel.Amount;
			}

			return Ok(await _basketRepository.UpdateBasket(shoppingCart));
		}

		[HttpDelete("{userName}", Name = "DeleteBasket")]
		[ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteBasket(string userName)
		{
			await _basketRepository.DeleteBasket(userName);

			return Ok();
		}

		[Route("[action]")]
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status202Accepted)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
		{
			var basket = await _basketRepository.GetBasket(basketCheckout.UserName);

			if (basket == null)
			{
				return BadRequest();
			}

			var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);

			await _basketRepository.DeleteBasket(basketCheckout.UserName);

			return Accepted();
		}
	}
}
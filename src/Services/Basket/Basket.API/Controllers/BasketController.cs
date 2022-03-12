using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
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
		private readonly IPublishEndpoint _publishEndpoint;

		public BasketController(IBasketRepository basketRepository, 
			DiscountGrpcService discountGrpcService, 
			IMapper mapper, 
			IPublishEndpoint publishEndpoint)
		{
			_basketRepository = basketRepository;
			_discountGrpcService = discountGrpcService;
			_mapper = mapper;
			_publishEndpoint = publishEndpoint;
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
			// get existing basket
			var basket = await _basketRepository.GetBasket(basketCheckout.UserName);

			if (basket == null)
			{
				return BadRequest();
			}

			// send BasketCheckoutEvent to rabbitMQ
			var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
			eventMessage.TotalPrice = basket.TotalPrice;
			await _publishEndpoint.Publish(eventMessage);

			// remove the basket
			await _basketRepository.DeleteBasket(basketCheckout.UserName);

			return Accepted();
		}
	}
}
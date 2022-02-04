using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System.Net;

namespace Ordering.API.Controllers
{
	[ApiController]
	[Route("api/vi/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{userName}", Name = "GetOrder")]
		[ProducesResponseType(typeof(IEnumerable<OrdersVm>), StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersList(string userName)
		{
			var query = new GetOrdersListQuery(userName);

			var orders = await _mediator.Send(query);

			return Ok(orders);
		}
	}
}
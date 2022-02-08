using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Respositories;
using Ordering.Domain.Entities;
using Ordering.Domain.Exceptions;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateOrderCommandHandler> _logger;

		public UpdateOrderCommandHandler(IOrderRepository orderRepository,
			IMapper mapper,
			ILogger<UpdateOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

			if (orderToUpdate == null)
			{
				throw new CustomNotFoundException(nameof(Order), request.Id);
			}

			//_mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
			_mapper.Map(request, orderToUpdate);

			await _orderRepository.UpdateAsync(orderToUpdate);

			_logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");

			return Unit.Value;
		}
	}
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
	public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly IEmailService _emailService;
		private readonly ILogger<CheckoutOrderCommand> _logger;

		public CheckoutOrderCommandHandler(IOrderRepository orderRepository,
			IMapper mapper,
			IEmailService emailService,
			ILogger<CheckoutOrderCommand> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_emailService = emailService;
			_logger = logger;
		}

		public Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}

using AutoMapper;
using Discount.API.Protos;
using Discount.API.Repositories;
using Grpc.Core;

namespace Discount.API.Services
{
	public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
	{
		private readonly IDiscountRepository _discountRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<DiscountService> _logger;

		public DiscountService(IDiscountRepository discountRepository, IMapper mapper, ILogger<DiscountService> logger)
		{
			_discountRepository = discountRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await _discountRepository.GetDiscount(request.ProductName);

			if (coupon == null)
			{
				throw new RpcException
					(new Status(StatusCode.NotFound,$"Discount with ProductName={request.ProductName} is not found."));
			}

			_logger.LogInformation($"Discount is retrieved for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");

			var couponModel = _mapper.Map<CouponModel>(coupon);

			return couponModel;
		}

		public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			return base.CreateDiscount(request, context);
		}

		public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			return base.UpdateDiscount(request, context);
		}

		public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			return base.DeleteDiscount(request, context);
		}
	}
}

using Discount.API.Protos;

namespace Basket.API.GrpcServices
{
	public class DiscountGrpcService
	{
		private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

		public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
		{
			_discountProtoServiceClient = discountProtoServiceClient;
		}

		public async Task<CouponModel> GetDiscountAsync(string productName)
		{
			var result = await _discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest()
			{
				ProductName = productName
			});

			return result;
		}
	}
}
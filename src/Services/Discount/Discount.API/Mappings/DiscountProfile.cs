using AutoMapper;
using Discount.API.Entities;
using Discount.API.Protos;

namespace Discount.API.Mappings
{
	public class DiscountProfile : Profile
	{
		public DiscountProfile()
		{
			CreateMap<Coupon, CouponModel>().ReverseMap();
		}
	}
}

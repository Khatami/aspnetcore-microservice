using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.Events;

namespace Ordering.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<BasketCheckoutEvent, BasketCheckout>().ReverseMap();
		}
	}
}
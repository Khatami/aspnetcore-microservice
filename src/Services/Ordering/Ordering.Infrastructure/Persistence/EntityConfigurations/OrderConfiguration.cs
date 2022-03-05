using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence.EntityConfigurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(q => q.UserName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(q => q.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(q => q.LastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(q => q.EmailAddress)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(q => q.AddressLine)
				.HasMaxLength(50);

			builder.Property(q => q.Country)
				.HasMaxLength(50);

			builder.Property(q => q.State)
				.HasMaxLength(50);

			builder.Property(q => q.ZipCode)
				.HasMaxLength(50);

			builder.Property(q => q.CardName)
				.HasMaxLength(50);

			builder.Property(q => q.CardNumber)
				.HasMaxLength(50);

			builder.Property(q => q.Expiration)
				.HasMaxLength(50);

			builder.Property(q => q.CVV)
				.HasMaxLength(4);

			builder.Property(q => q.PaymentMethod);
		}
	}
}

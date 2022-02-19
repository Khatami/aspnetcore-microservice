using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Respositories;
using Ordering.Application.Models;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Respositories;

namespace Ordering.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<OrderContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

			services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
			services.AddScoped<IOrderRepository, OrderRepository>();

			services.AddTransient<IEmailService, EmailService>();

			// Options pattern
			services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));

			return services;
		}
	}
}

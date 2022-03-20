using EventBus.Messages.Common;
using Gelf.Extensions.Logging;
using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// greylog
builder.Services.Configure<GelfLoggerOptions>(builder.Configuration.GetSection("GrayLog"));
builder.Host.ConfigureLogging(logging =>
{
	logging.AddGelf();
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MassTransit - RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{
	config.AddConsumer<BasketCheckoutConsumer>();

	config.UsingRabbitMq((context, config) =>
	{
		config.Host(builder.Configuration["EventBusSettings:HostAddress"]);

		config.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, configure =>
		{
			configure.ConfigureConsumer<BasketCheckoutConsumer>(context);
		});
	});
});

//Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();
app.MigrateDatabase<OrderContext>((context, services) =>
{
	var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();

	OrderContextSeed.SeedAsync(context, logger).Wait();
}, 20);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
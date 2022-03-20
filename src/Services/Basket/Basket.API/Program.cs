using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.API.Protos;
using Gelf.Extensions.Logging;
using MassTransit;
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

// Redis Configuration
var connectionString = builder.Configuration.GetSection("CacheSettings").GetValue<string>("ConnectionString");
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = connectionString;
});

// Grpc Configuration
var discountGrpcUrl = builder.Configuration.GetSection("GrpcSettings").GetValue<string>("DiscountUrl");
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(option =>
{
	option.Address = new Uri(discountGrpcUrl);
});

// MassTransit - RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
	config.UsingRabbitMq((context, config) => {
		config.Host(builder.Configuration["EventBusSettings:HostAddress"]);
	});
});

// Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
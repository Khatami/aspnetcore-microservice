using Discount.API.Extensions;
using Discount.API.Repositories;
using Discount.API.Services;
using Gelf.Extensions.Logging;
using Npgsql;
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
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<NpgsqlConnection>(service =>
{
	string connectionString = builder.Configuration.GetSection("DatabaseSettings")
		.GetValue<string>("ConnectionString");

	return new NpgsqlConnection(connectionString);
});

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MigrateDatabase<Program>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.MapGrpcService<DiscountService>();

app.Run();
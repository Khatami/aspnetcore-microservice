using Discount.API.Extensions;
using Discount.API.Repositories;
using Discount.API.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<NpgsqlConnection>(service =>
{
	string connectionString = builder.Configuration.GetSection("DatabaseSettings")
		.GetValue<string>("ConnectionString");

	return new NpgsqlConnection(connectionString);
});

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
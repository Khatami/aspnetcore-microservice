using Catalog.API.Data;
using Catalog.API.Repositories;
using Gelf.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// greylog
builder.Services.Configure<GelfLoggerOptions>(builder.Configuration.GetSection("GrayLog"));
builder.Host.ConfigureLogging(logging =>
{
	logging.AddGelf();
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRepositories();

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

public static class Extensions
{
	public static void AddRepositories(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddScoped<IProductRepository, ProductRepository>();
		serviceCollection.AddScoped<ICatalogContext, CatalogContext>();
	}
}
using Gelf.Extensions.Logging;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// graylog
builder.Services.Configure<GelfLoggerOptions>(builder.Configuration.GetSection("GrayLog"));
builder.Host.ConfigureLogging(logging =>
{
	logging.AddGelf();
});

// ocelot
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
	config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
});

builder.Services
	.AddOcelot()
	.AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
	endpoints.MapGet("/", async context =>
	{
		await context.Response.WriteAsync("Ocelot API Gateway.fet");
	});
});

app.UseOcelot();

app.Run();

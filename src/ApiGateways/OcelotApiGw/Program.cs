using Gelf.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
	config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
});

// graylog
builder.Services.Configure<GelfLoggerOptions>(builder.Configuration.GetSection("GrayLog"));
builder.Host.ConfigureLogging(logging =>
{
	logging.AddGelf();
});

// ocelog
builder.Services.AddOcelot();

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

using Gelf.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// graylog
builder.Services.Configure<GelfLoggerOptions>(builder.Configuration.GetSection("GrayLog"));
builder.Host.ConfigureLogging(logging =>
{
	logging.AddGelf();
});

// ocelog
builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot();

app.Run();

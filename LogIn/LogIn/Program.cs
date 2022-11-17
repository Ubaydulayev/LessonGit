using LogIn.Loggers;
using Serilog;
using TelegramSink;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddProvider(new MyLoggerProvider());

var logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "Log.txt",
        fileSizeLimitBytes: 20,
        rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .WriteTo.TeleSink("5672623294:AAFka8H39n0sOJ7OETdiLtbuUfD6DWiTw7U", "352669747")
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
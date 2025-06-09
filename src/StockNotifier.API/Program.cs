using Hangfire;
using StockNotifier.Application;
using StockNotifier.Infrastructure.SignalRServer;
using static StockNotifier.Application.BuilderExtensions;
using static StockNotifier.Infrastructure.BuilderExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost7196",
        policy =>
        {
            policy.WithOrigins("https://localhost:7196")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAnyOrigin", p => p
        .WithOrigins("null") // Origin of an html file opened in a browser
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowLocalhost7196");
app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.MapHub<AlertHub>("/alert-hub");

app.Run();

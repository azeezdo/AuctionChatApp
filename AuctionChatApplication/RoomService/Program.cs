using AuctionChatApplication.RoomService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext - SQL Server (connection string from configuration)
builder.Services.AddDbContext<RoomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RoomServiceDb")));

// MassTransit (RabbitMQ)
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var host = builder.Configuration["RABBIT_HOST"] ?? "rabbitmq";
        cfg.Host(host, "/", h => { h.Username("guest"); h.Password("guest"); });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.Run();
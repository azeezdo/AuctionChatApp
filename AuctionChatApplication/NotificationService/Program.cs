using MassTransit;
using NotificationService.Consumers;
using NotificationService.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BidPlacedConsumer>();
    x.AddConsumer<AuctionStartedConsumer>();
    x.AddConsumer<AuctionEndedConsumer>();
    x.AddConsumer<InvoiceCreatedConsumer>();
    x.AddConsumer<PaymentCompletedConsumer>();


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


app.MapControllers();
app.MapHub<AuctionHub>("/hubs/auction");


app.Run();
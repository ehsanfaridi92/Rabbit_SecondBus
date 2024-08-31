
var builder = Host.CreateApplicationBuilder(args);

//config first bus for consume message
var rabbit1Configuration = builder.Configuration.GetSection("Rabbit1Configuration").Get<RabbitConfiguration>();

builder.Services.AddMassTransit(busServiceCollection =>
{
    busServiceCollection.AddConsumer<SomeConsumer>();

    busServiceCollection.UsingRabbitMq((busContext, busConfigurator) =>
    {
        busConfigurator.Host(rabbit1Configuration.Connection);

        busConfigurator.ConfigureEndpoints(busContext);
    });
});

//config second bus for publish message
var rabbit2Configuration = builder.Configuration.GetSection("Rabbit2Configuration").Get<RabbitConfiguration>();

builder.Services.AddMassTransit<ISecondBus>(x =>
{
    x.UsingRabbitMq((_, cfg) =>
    {
        cfg.Host(rabbit2Configuration.Connection);
    });
});

var host = builder.Build();
host.Run();

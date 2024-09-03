using WhatsAppNotificationWorker;
using WhatsAppNotificationWorker.Options;
using WhatsAppNotificationWorker.services.Interfaces;
using WhatsAppNotificationWorker.services.Providers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<IWhatsAppService, WhatsAppService>();
builder.Services.AddOptions<RabbitMqConfig>()
    .BindConfiguration(nameof(RabbitMqConfig));
builder.Services.AddOptions<TwilioConfig>()
    .BindConfiguration(nameof(TwilioConfig));
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
await host.RunAsync();
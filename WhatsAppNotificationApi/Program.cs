using WhatsAppNotificationApi.CustomFilters;
using WhatsAppNotificationApi.Options;
using WhatsAppNotificationApi.services.Interfaces;
using WhatsAppNotificationApi.services.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<CustomValidationFilter>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.AddOptions<RabbitMqConfig>()
    .BindConfiguration(nameof(RabbitMqConfig))
    .ValidateDataAnnotations()
    .ValidateOnStart();
builder.Services.AddOptions<TwilioConfig>()
    .BindConfiguration(nameof(TwilioConfig))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
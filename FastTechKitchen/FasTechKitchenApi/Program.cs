using FasTechKitchen.Api.Filters;
using FasTechKitchen.Api.Logging;
using FasTechKitchen.Api.Robots.RabbitMQ;
using FastTech.Contracts.DataTransferObjects;
using FastTechKitchen.Application.Interfaces;
using FastTechKitchen.Application.Services;
using FastTechKitchen.Domain.Entities;
using FastTechKitchen.Domain.Interfaces;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using FastTechKitchen.Domain.Services;
using FastTechKitchen.Domain.Settings;
using FastTechKitchen.Infraestructure.Data;
using FastTechKitchen.Infraestructure.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using static FastTechKitchen.Domain.Constants.AppConstants;


var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Configuration
    .AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<FastTechSettings>(builder.Configuration);

//builder.Services.AddHealthChecks().ForwardToPrometheus();
builder.WebHost.UseUrls("http://localhost:5056");

builder.Services.AddControllers(options => options.Filters.Add<UserFilter>()).AddNewtonsoftJson(options =>
{
    var settings = options.SerializerSettings;
    settings.NullValueHandling = NullValueHandling.Ignore;
    settings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
    settings.FloatParseHandling = FloatParseHandling.Double;
    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    settings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
    settings.Culture = new CultureInfo("en-US");
    settings.Converters.Add(new StringEnumConverter());
    settings.ContractResolver = new DefaultContractResolver(); // usa PascalCase (padrão)
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FastTechKitchen API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.CustomSchemaIds(type =>
    {
        var namingStrategy = new SnakeCaseNamingStrategy();
        return namingStrategy.GetPropertyName(type.Name, false);
    });
       
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AllowNullDestinationValues = true;
    cfg.AllowNullCollections = true;
    cfg.AddMaps(typeof(BaseModel).Assembly);
});

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SQLConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,                // tenta até 5 vezes
            maxRetryDelay: TimeSpan.FromSeconds(10), // espera até 10s entre tentativas
            errorNumbersToAdd: null          // use os erros padrão do SQL Server
        )
    );

    options.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
    options.EnableSensitiveDataLogging();
});


builder.Services.AddMemoryCache();

// Inject all Services and repositories

#region Repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoItemCardapioRepository, PedidoItemCardapioRepository>();


#endregion

#region Services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPedidoItemCardapioService, PedidoItemCardapioService>();


#endregion

#region Application Services

builder.Services.AddScoped<IUserApplicationService, UserApplicationService>();
builder.Services.AddScoped<IPedidoApplicationService, PedidoApplicationService>();
builder.Services.AddScoped<IPedidoItemCardapioApplicationService, PedidoItemCardapioApplicationService>();

#endregion

#region Filters

builder.Services.AddScoped<IAuthorizationFilter, UserFilter>();
builder.Services.AddScoped(x => new UserData());

#endregion

#region MassTransit
// 1. Adicionar o Consumer que criaremos a partir do PedidoApplicationService
builder.Services.AddMassTransit(x =>
{
    // Adiciona o consumidor. Substitua 'PedidoConsumer' pelo nome do seu novo consumidor.
    x.AddConsumer<PedidoConsumer>(); // <--- NOVO CONSUMER (ver item 6)

    x.UsingRabbitMq((context, cfg) =>
    {
        // Obtém a string de conexão do appsettings.json
        var rabbitMqHost = builder.Configuration.GetConnectionString("RabbitMq");

        // Configura a conexão com o RabbitMQ usando a URL completa
        cfg.Host(new Uri(rabbitMqHost));

        // Configura o endpoint de recebimento (a fila) para o PedidoConsumer
        cfg.ReceiveEndpoint("fastech.pedido", e => // Nome da fila (QueueName do antigo RabbitMQ)
        {
            e.ConfigureConsumer<PedidoConsumer>(context);
        });
    });
});
#endregion



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FastTechKitchen API 2025"));

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDBContext>();

    context.Database.Migrate();
}


app.UseHealthChecks("/health");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

using FastTech.Api.Filters;
using FastTech.Api.Logging;
using FastTech.Application.Interfaces;
using FastTech.Application.Services;
using FastTech.Contracts.DataTransferObjects;
using FastTech.Domain.Entities;
using FastTech.Domain.Interfaces;
using FastTech.Domain.Interfaces.Infrastructure;
using FastTech.Domain.Services;
using FastTech.Domain.Settings;
using FastTech.Infrastructure.Data;
using FastTech.Infrastructure.Data.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using FastTech.Application.Mappings;


var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Configuration
    .AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<FastTechSettings>(builder.Configuration);

builder.WebHost.UseUrls("http://localhost:5056");

builder.Services.AddControllers().AddNewtonsoftJson(options =>
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FastTech API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.CustomSchemaIds(type =>
    {
        var namingStrategy = new SnakeCaseNamingStrategy();
        return namingStrategy.GetPropertyName(type.Name, false);
    });
   
});

builder.Services.AddAutoMapper((sp, cfg) =>
{
    cfg.AllowNullDestinationValues = true;
    cfg.AllowNullCollections = true;
    cfg.ConstructServicesUsing(sp.GetService);
}, Assembly.GetAssembly(typeof(BaseModel)));

builder.Services.AddAutoMapper(typeof(ItemCardapioMapper));

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
    options.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
    options.EnableSensitiveDataLogging();
});


builder.Services.AddMemoryCache();

// Inject all Services and repositories

#region Repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IItemCardapioRepository, ItemCardapioRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();


#endregion

#region Services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IItemCardapioService, ItemCardapioService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
//builder.Services.AddScoped<IPedidoProducerService, PedidoProducerService>();


#endregion

#region Application Services

builder.Services.AddScoped<IUserApplicationService, UserApplicationService>();
builder.Services.AddScoped<IPedidoApplicationService, PedidoApplicationService>();
builder.Services.AddScoped<IItemCardapioApplicationService, ItemCardapioApplicationService>();

#endregion

#region Filters

builder.Services.AddScoped(x => new UserData());

#endregion

#region Mass Transit

builder.Services.AddMassTransit(x =>
{
    // Adiciona o consumidor à lista de consumidores do MassTransit
    x.AddConsumer<PedidoConsumerService>();
    
    x.UsingRabbitMq((context, cfg) =>
    {
        // Conecta ao host RabbitMQ
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                
        // Configura o endpoint (fila) para o consumidor
        cfg.ReceiveEndpoint("fasttech.pedido", e =>
        {
            // Vincula o consumidor 'PedidoConsumer' a este endpoint
            e.ConfigureConsumer<PedidoConsumerService>(context);
        });
    });
});
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FastTech API 2025"));

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDBContext>();

    context.Database.Migrate();
}


app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

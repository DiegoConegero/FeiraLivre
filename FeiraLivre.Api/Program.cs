using FeiraLivre.Application.Interfaces;
using FeiraLivre.Application.Mapping;
using FeiraLivre.Application.Middleware;
using FeiraLivre.Application.Services;
using FeiraLivre.Core.Interfaces;
using FeiraLivre.Infrastructure.DbContext;
using FeiraLivre.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole().AddConfiguration();

//builder.Logging.AddProvider()
var loggerFactory = LoggerFactory.Create(lb => lb.AddConfiguration(builder.Configuration));

//builder.WebHost.ConfigureLogging((hostingContext, logging) =>
//{
//    logging.AddFilter("path/to/Logs/myapp-{Date}.txt");
//});

//builder.Logging.AddConfiguration(new )

LoggerFactory teste = new LoggerFactory();

// Add services to the container.
builder.Services.AddAutoMapper(typeof(FeiraLivreProfile));
builder.Services.AddTransient<IFeiraLivreRepository, FeiraLivreRepository>();
builder.Services.AddTransient<IFeiraLivreService, FeiraLivreService>();
builder.Services.AddTransient<IDbConnectionFeiraLivre, DbConnectionFeiraLivre>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<DbConfigurationFeiraLivre>(builder.Configuration.GetSection("FeiraLivreDatabase"));

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "TEste Catálogo de feiras livres de São Paulo",
            Version = $"{builder.Environment.EnvironmentName} - v1 - {DateTime.Now}",
            Description = "Consulta e manutenção de feiras livres de São Paulo",
            Contact = new OpenApiContact
            {
                Name = "Diego Munhoz Conegero",
                //Url     = new Uri(SwaggerConstante.UrlContato),
                Email = "diego.conegero@gmail.com"
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

var app = builder.Build();


//ILogger<ErrorHandlerMiddleware> logger = app.Services.GetRequiredService<ILogger<ErrorHandlerMiddleware>>();
//logger.LogTrace()

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("../swagger/v1/swagger.json", "v1");
        //options.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();

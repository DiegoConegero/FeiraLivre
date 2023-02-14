using FeiraLivre.Api.Mapping;
using FeiraLivre.Application.Middleware;
using FeiraLivre.Application.UseCases;
using FeiraLivre.Infrastructure.DbContext;
using FeiraLivre.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Serilog;
using Serilog.Formatting.Json;
using FeiraLivre.Core.Interfaces.Repository;
using FeiraLivre.Core.Interfaces.UseCases;
using FluentValidation;
using FeiraLivre.Core.Entities.Validators;
using FeiraLivre.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
var filePath = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).FullName}\LogTrace\Log";
var logPath = $"{filePath}.{builder.Configuration.GetSection("LogFile").GetSection("Extensao").Value}";

builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.WriteTo.File(new JsonFormatter(), logPath, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose);
}, true);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(FeiraLivreProfile));
builder.Services.AddTransient<IFeiraLivreRepository, FeiraLivreRepository>();
builder.Services.AddTransient<IFeiraLivreUseCase, FeiraLivreUseCase>();
builder.Services.AddTransient<IDbConnectionFeiraLivre, DbConnectionFeiraLivre>();
builder.Services.AddTransient<IValidator<FeiraLivreEntity>, FeiraLivreEntityValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<DbConfigurationFeiraLivre>(builder.Configuration.GetSection("FeiraLivreDatabase"));

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title       = "Catálogo de feiras livres de São Paulo",
            Version     = $"{builder.Environment.EnvironmentName} - v1 - {DateTime.Now}",
            Description = "Consulta e manutenção de feiras livres de São Paulo",
            Contact     = new OpenApiContact
            {
                Name    = "Diego Munhoz Conegero",
                Url     = new Uri("https://www.linkedin.com/in/diego-munhoz-conegero-4b7402b1/"),
                Email   = "diego.conegero@gmail.com"
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

var app = builder.Build();

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

using FCG.Catalog.Api.Middlewares;
using FCG.Catalog.Application.Commands.BibliotecaCommand.AdicionarJogoCommand;
using FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;
using FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;
using FCG.Catalog.Application.Queries;
using FCG.Catalog.Core.Behaviors;
using FCG.Catalog.Core.UnitOfWork;
using FCG.Catalog.Infra;
using FCG.Catalog.Infra.Queries;
using FCG.Catalog.Infra.Rabbitmq.Consumers;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configuração do DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FcgCatalogDbContext>(options => options.UseSqlServer(connectionString));

// Configuração do MassTransit com RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentProcessedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            builder.Configuration["RabbitMQ:Host"],
            builder.Configuration["RabbitMQ:VirtualHost"],
            h =>
            {
                h.Username(builder.Configuration["RabbitMQ:Username"!]);
                h.Password(builder.Configuration["RabbitMQ:Password"]);
            });

        cfg.ConfigureEndpoints(context);
    });
});



#region DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<FcgCatalogDbContext>>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Query Services
builder.Services.AddScoped<IJogoQueryService, JogoQueryService>();
builder.Services.AddScoped<IOrdemCompraQueryService, OrdemCompraQueryService>();
builder.Services.AddScoped<IBibliotecaQueryService, BibliotecaQueryService>();

#endregion

#region MEDIATOR
//Jogo
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CadastrarJogoHandler).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(CadastrarJogoValidator).Assembly);

//Biblioteca
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AdicionarJogoHandler).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(AdicionarJogoValidator).Assembly);

//OrdemCompra
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CriarOrdemCompraHandler).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(CriarOrdemCompraValidator).Assembly);
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FcgCatalogDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

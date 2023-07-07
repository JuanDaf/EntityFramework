using ApiWebExamenEf.Models;
using ApiWebExamenEf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<Contexto>(builder.Configuration.GetConnectionString("ServiciosDb"));

var app = builder.Build();


app.MapGet("/Api/Personas", async ([FromServices] Contexto contexto) =>
{
    return Results.Ok(contexto.personas.Select(p => p));
});

app.MapPost("/Api/CargarPersona", async ([FromServices] Contexto contexto , [FromBodyAttribute] Persona persona) =>
{
    await contexto.AddAsync(persona);
    await contexto.SaveChangesAsync();  
    return Results.Ok();
});

app.MapPost("/Api/CargarServicio", async ([FromServices] Contexto contexto, [FromBodyAttribute] Servicio servicio) =>
{
    await contexto.AddAsync(servicio);
    await contexto.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/", () => "Hello World!");



app.MapGet("/DbConecction", async ([FromServices] Contexto contexto) =>
{
    contexto.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + contexto.Database.IsInMemory());
});

app.Run();

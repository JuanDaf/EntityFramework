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
app.MapGet("/Api/Servicos", async ([FromServices] Contexto contexto) =>
{
    return Results.Ok(contexto.servicios.Select(p => p));
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

app.MapPost("/Api/CargarDetallesPaS", async ([FromServices] Contexto contexto, [FromBodyAttribute] DetallePaS detallePaS) =>
{
    await contexto.AddAsync(detallePaS);
    await contexto.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("/Api/ActulizarPersona/{id}", async ([FromServices] Contexto contexto, [FromBodyAttribute] Persona persona, [FromRoute] string id ) =>
{
    var personaActualizar = contexto.personas.Find(id);
    if (personaActualizar != null)
    {
        personaActualizar.Nombres = persona.Nombres;
        personaActualizar.Apellidos = persona.Apellidos;
        personaActualizar.Email = persona.Email;
        personaActualizar.Direccion = persona.Direccion;
        personaActualizar.NivelEstrato = persona.NivelEstrato;

        await contexto.SaveChangesAsync();
        return Results.Ok();
    }
   
    return Results.NotFound();
});


app.MapDelete("/Api/EliminarPersona/{id}", async ([FromServices] Contexto contexto, [FromRoute] string id) =>
{
    var personaActual = contexto.personas.Find(id);
    if (personaActual != null)
    {
        contexto.Remove(personaActual);
        await contexto.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapDelete("/Api/EliminarServicio/{id}", async ([FromServices] Contexto contexto, [FromRoute] Guid id) =>
{
    var servicioActual = contexto.servicios.Find(id);
    if (servicioActual != null)
    {
        contexto.Remove(servicioActual);
        await contexto.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapGet("/", () => "Hello World!");



app.MapGet("/DbConecction", async ([FromServices] Contexto contexto) =>
{
    contexto.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + contexto.Database.IsInMemory());
});

app.Run();

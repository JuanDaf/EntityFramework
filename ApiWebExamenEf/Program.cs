using ApiWebExamenEf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<Contexto>(builder.Configuration.GetConnectionString("ServicosDb"));

var app = builder.Build();

app.MapGet("/DbConecction", async ([FromServices] Contexto contexto) => {
    contexto.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + contexto.Database.IsInMemory());
});

app.Run();

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Swagger + Endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthChecks("/healthz");

// Redirige a Swagger si alguien entra a "/"
app.MapGet("/", () => Results.Redirect("/swagger"));

// Endpoints de la calculadora (usa decimal para precisión en números con coma)
app.MapGet("/api/sum", ([FromQuery] decimal a, [FromQuery] decimal b) =>
{
    return Results.Ok(new { operation = "sum", a, b, result = a + b });
})
.WithName("Sumar")
.Produces(StatusCodes.Status200OK);

app.MapGet("/api/sub", ([FromQuery] decimal a, [FromQuery] decimal b) =>
{
    return Results.Ok(new { operation = "sub", a, b, result = a - b });
})
.WithName("Restar")
.Produces(StatusCodes.Status200OK);

app.MapGet("/api/mul", ([FromQuery] decimal a, [FromQuery] decimal b) =>
{
    return Results.Ok(new { operation = "mul", a, b, result = a * b });
})
.WithName("Multiplicar")
.Produces(StatusCodes.Status200OK);

app.MapGet("/api/div", ([FromQuery] decimal a, [FromQuery] decimal b) =>
{
    if (b == 0) return Results.BadRequest(new { error = "No se puede dividir entre cero." });
    return Results.Ok(new { operation = "div", a, b, result = a / b });
})
.WithName("Dividir")
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

// Escuchar
app.Run();

using Microsoft.OpenApi.Models;
using System; 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v0.1", new OpenApiInfo { Title = "BLOC4-API", Description = "Routes de l'API utilisées par le logiciel de gestion des coordonnées utilisateurs" });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/V0.1/swagger.json", "BLOC4-API V0.1");
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();
app.MapControllers();


app.Run();

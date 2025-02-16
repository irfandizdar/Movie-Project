﻿using Microsoft.Extensions.Configuration;
using Movie_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()   // Ovo omogućava pristup iz svih izvora
            .AllowAnyMethod()   // Dopustiti sve HTTP metode (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader()); // Dopustiti sve HTTP zaglavlja
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();



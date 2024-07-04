using FastEndpoints;

using POS.Api.Apis;
using POS.Persistence;
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddPersistence();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();
builder.Services.AddProblemDetails();

builder.Services.AddPersistenceServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseFastEndpoints();

app.MapCustomerEndpoints();

app.MapProductEndpoints();

app.MapOrderEndpoints();

app.Run();

public partial class Program {}

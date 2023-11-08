using System.Reflection;
using BabyCRM.Extensions;
using BLL.Commands.Clients;
using BLL.Commands.Filials;
using BLL.Managers;
using DAL.DbManagers;
using DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureServices();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.RunWithExceptionsHandling(app.Logger);

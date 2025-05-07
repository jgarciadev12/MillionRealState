using Persistence;
using Web.API.Endpoints;
using Application;
using Persistence.Context;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
    await new ServiceSeeding().SeedAsync(context); 
}

app.UseCors("ReactCrosPolicity");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseErrorHandler();
app.UseAuthorization();

app.MapControllers();

app.Run();
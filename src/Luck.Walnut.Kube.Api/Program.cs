using Luck.Framework.Infrastructure;
using Luck.Walnut.Kube.Api.AppModules;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication<AppWebModule>();
builder.Services.AddMediatR(AssemblyHelper.AllAssemblies);
var app = builder.Build();
app.UsePathBase("/kube");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.InitializeApplication();
app.Run();


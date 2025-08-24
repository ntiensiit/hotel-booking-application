using System.Text.Json;
using System.Text.Json.Serialization;
using Adapter.Driven.NHibernate.Helpers;
using Adapter.Driving.ResourceServer.Converter;
using Adapter.Driving.ResourceServer.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationMediatR()
    .AddNHibernate(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddApplicationAuthentication(builder.Configuration);

NHibernateHelper.OpenSession();

builder.Services.AddControllers(options => { options.Filters.Add(new AuthorizeFilter()); }).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
using Application.Interfaces;
using Application.Products;
using Domain.Entities.Product;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.Endpoints.Product;
using System;
using System.Reflection;
using static Application.Products.Dto.ProductDto;
using FluentValidation;
using Application.Products.Command.CreateUpdateCommand;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PmsConnection")));

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IBaseRepository<Product>, BaseRepository<Product, PmsDbContext>>();
builder.Services.AddScoped<IPmsRepository<Product>, PmsRepository<Product>>();
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

builder.Services.AddScoped<IValidator<ProductCommand>, ProductCommandValidator>();
builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowSpecificOrigin", policy =>
    //{
    //    policy.WithOrigins("http://localhost:4200")
    //          .AllowAnyHeader()
    //          .AllowAnyMethod();
    //});
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapProductEndpoints();


app.Run();

using FluentValidation;
using FluentValidation.AspNetCore;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Infrastructure.Validators;
using WebAPI.Interfaces;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Context>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers()
                .AddNewtonsoftJson();  // для PATCH-запроса

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

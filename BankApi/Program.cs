using BankApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BankApi;
using BankApi.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using BankApi.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Подключение к Redis
// добавление кэширования
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "local";
});

builder.Services.AddMemoryCache();

// Получение строки подключения к бд из конфигурации 
var connection = builder.Configuration.GetConnectionString("MyCon");
// Настройка зависимости, добавляет при необходимости обьект MyContext
builder.Services.AddDbContext<MyContext>(options => options.UseNpgsql(connection));
// Настройка зависимости, добавляет при необходимости обьект Repository
builder.Services.AddScoped<RepositoryUser>();
builder.Services.AddScoped<RepositoryCard>();
builder.Services.AddScoped<RepositoryTransaction>();
builder.Services.AddScoped<ServicesCard>();

// Настройки для работы с JWT токенами
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // Укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // Строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // Будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // Установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // Будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // Установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // Валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });

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

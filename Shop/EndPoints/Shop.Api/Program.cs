using Common.AspNetCore.Middlewares;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Config;
using Shop.Presentation.Facade;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");

var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
builder.Services.AddSingleton(jwtConfig);

builder.Services.RegisterShopDependency(connectionString);
builder.Services.InitFacadeDependency();

var app = builder.Build();

app.UseApiCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
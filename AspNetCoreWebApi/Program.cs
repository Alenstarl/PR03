using AspNetCoreWebApi.Data;
using AspNetCoreWebApi.Services;
using AspNetCoreWebApi.Sql.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ProductCrudService>();
builder.Services.AddTransient<TestService>();
builder.Services.AddTransient<SchoolCrudService>();

// Set as singleton to provide static data.
builder.Services.AddSingleton<ProductData>();

builder.Services.AddDbContext<TurboBootcampDbContext>(options =>
    // Use Configuration.GetConnectionString to obtain the setting value in appsettings.
    options.UseNpgsql(builder.Configuration.GetConnectionString("Sql")));

// Configure JWT validation for web API's authentication and authorization.
// Everytime a request was made to a protected web API, it will request to Duende and Duende will validate the JWT token in HTTP header "Authorize".
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://demo.duendesoftware.com";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "localhost",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          // Bypass all other rules for convenient. Do not do this in real project scenarios.
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("localhost");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

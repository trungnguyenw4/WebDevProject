using Microsoft.EntityFrameworkCore;
using WebDevelopmentProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebDevProject.Controllers;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using static Microsoft.Extensions.DependencyInjection.IServiceCollection;
using static Microsoft.Extensions.Configuration.IConfiguration;
using System.Configuration;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Serilog.Extensions.Logging;

using Serilog;
using Serilog.Formatting.Json;
//var configuration = Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//builder.Services.AddDbContext<InsuranceContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

//substitute this one with the one in appsetting.json to have the cloud version of the program
//"ConnectionStrings": {
//  "Connection": "Server=tcp:insuranceplatform.database.windows.net,1433;Initial Catalog=NewDatabaseForInsurancePlatform;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;"
//},


builder.Services.AddDbContext<InsuranceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnectionLocal")));    

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<InsuranceContext>().AddDefaultTokenProviders();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<RolesController>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                    });





var configuration = builder.Configuration;

        // Configure services
      

        Log.Logger = new LoggerConfiguration()
        .WriteTo.File(configuration["Logging:PathFormat"])
        .CreateLogger();

     builder.Services.AddLogging(loggingBuilder =>
        {
            // Add Serilog as a logging provider
            loggingBuilder.AddSerilog();
        });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
//app.UseHttpsRedirection();


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}



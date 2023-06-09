using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RefreshJWT.API.Models;
using RefreshJWT.API.Repositories;
using RefreshJWT.API.Services;
using RefreshJWT.API.Utils;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

// Configure Services
{
    var services = builder.Services;

    services.AddDbContext<RefreshTokenContext>(options => options.UseInMemoryDatabase("TestDb"));

    services.AddTransient<IJwt, Jwt>();
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IUserService, UserService>();
    
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(conf => 
            conf.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["JWTConfiguration:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JWTConfiguration:Audience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfiguration:Key"])),
                ClockSkew = TimeSpan.Zero
            }
        );

    services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RefreshTokenContext>();    
    var testUser = new User
    {
        FirstName = "Admin",
        LastName = "",
        Username = "admin",
        Email = "admin@domain.com",
        Password =  BCrypt.Net.BCrypt.HashPassword("Abc.123456")
    };
    context.User.Add(testUser);
    context.SaveChanges();
}

// Configure the HTTP request pipeline.
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(conf => 
        conf.SetIsOriginAllowed(origin => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();

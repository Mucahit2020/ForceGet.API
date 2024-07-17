using ForceGetCase.Core.Map;
using ForceGetCase.Core.Models.User;
using ForceGetCase.Core.Services.Abstracts;
using ForceGetCase.Core.Services.Concrates;
using ForceGetCase.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ForceGetDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ForceGetConnection"),
                     new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<IComboBoxService, ComboBoxService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IAuthService, AuthService>();





builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var authSettings = builder.Configuration.GetSection("AuthSettings").Get<AuthSettings>();
        if (authSettings == null)
        {
            throw new Exception("AuthSettings configuration section is missing or empty.");
        }
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authSettings.Issuer,
            ValidAudience = authSettings.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret))
        };
    });




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(ModelToEntityProfile));

var app = builder.Build();


app.UseCors(c => c.WithOrigins(new string[] { "http://localhost:4200" })
    .AllowAnyMethod()
    .AllowCredentials()
    .AllowAnyHeader());
app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

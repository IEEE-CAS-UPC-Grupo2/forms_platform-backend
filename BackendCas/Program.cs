using BackendCas.IOC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InyectionDependencies(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewRule", app =>
    {
        app.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
});

// Retrieve the JWT key from configuration
var key = builder.Configuration.GetValue<string>("JwtSettings:key");

// Ensure the key is at least 256 bits (32 bytes)
if (string.IsNullOrEmpty(key) || Encoding.ASCII.GetBytes(key).Length < 32)
{
    throw new ArgumentException("La clave JWT debe tener al menos 256 bits (32 bytes).");
}

var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Elimina el tiempo de gracia predeterminado de 5 minutos
    };
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("NewRule");

app.MapControllers();

app.Run();
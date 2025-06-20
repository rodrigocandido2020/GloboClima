using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GloboClima.API;
using GloboClima.API.ProgramStart;
using GloboClima.Dominio.Interfaces;
using GloboClima.Servico.Servicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var frontEndCorsPolicy = "FrontEndPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: frontEndCorsPolicy,
        policy =>
        {
            policy.WithOrigins("https://localhost:7108")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<ServicoToken>(sp =>
    new ServicoToken(
        builder.Configuration["Jwt:SecretKey"],
        builder.Configuration["Jwt:Issuer"],
        builder.Configuration["Jwt:Audience"]
    )
);
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions("AWS"));
builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var config = new AmazonDynamoDBConfig
    {
        RegionEndpoint = RegionEndpoint.SAEast1
    };

    return new AmazonDynamoDBClient(
        builder.Configuration["AWS:AccessKey"],
        builder.Configuration["AWS:SecretKey"],
        config
    );
});

ConfiguracaoDeInjecaoDeDependencia.BindServices(builder.Services);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "GloboClima API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new()
    {
        Description = "Insira o token JWT no formato: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CriarUsuarioAdmin>();
    await seeder.CriarUsuarioAdministrador();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigurarStatusCodesPage();

app.ConfigurarManipuladorDeExcecoes(app.Services.GetRequiredService<ILoggerFactory>());

app.UseHttpsRedirection();

app.UseCors(frontEndCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

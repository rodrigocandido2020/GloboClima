using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GloboClima.API;
using GloboClima.API.ProgramStart;
using GloboClima.Servico.Servicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CORS
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

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Autenticação JWT
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

// Token Service
builder.Services.AddSingleton<ServicoToken>(sp =>
    new ServicoToken(
        builder.Configuration["Jwt:SecretKey"],
        builder.Configuration["Jwt:Issuer"],
        builder.Configuration["Jwt:Audience"]
    )
);
// AWS DynamoDB (usa ~/.aws/credentials e região do config)
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions("AWS"));
builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var config = new AmazonDynamoDBConfig
    {
        RegionEndpoint = RegionEndpoint.SAEast1 // ajuste se necessário
    };

    return new AmazonDynamoDBClient(
        builder.Configuration["AWS:AccessKey"],
        builder.Configuration["AWS:SecretKey"],
        config
    );
});
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

// Serviços relacionados ao DynamoDB
builder.Services.AddSingleton<ServicoUsuario>();
builder.Services.AddSingleton<CriarUsuarioAdmin>();
builder.Services.AddSingleton<ServicoPaisClima>();

ConfiguracaoDeInjecaoDeDependencia.BindServices(builder.Services);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CriarUsuarioAdmin>();
    await seeder.CriarUsuarioAdminAsync();
}

// Dev pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigurarManipuladorDeExcecoes(app.Services.GetRequiredService<ILoggerFactory>());

app.UseHttpsRedirection();

app.UseCors(frontEndCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

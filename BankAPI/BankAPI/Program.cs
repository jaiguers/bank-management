using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BankAPI.CrossCutting.Authentication;
using BankAPI.Domain.Business.Interface;
using BankAPI.Domain.Business.Services;
using BankAPI.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Core.Configuration;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var urlAceptadas = builder.Configuration.GetSection("AllowedHosts").Value;
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(urlAceptadas)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDB = builder.Configuration.GetSection("MongoDbSettings").Get<BankAPI.Domain.Models.MongoDbSettings>();
builder.Services.Configure<BankAPI.Domain.Models.MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddDbContext<BankDbContext>(options => options.UseMongoDB(mongoDB.AtlasURL ?? "", mongoDB.DatabaseName ?? ""));

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

// MongoIdentity configuration
var mongoDbIndenityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new AspNetCore.Identity.MongoDbCore.Infrastructure.MongoDbSettings
    {
        ConnectionString = mongoDB.AtlasURL ?? "",
        DatabaseName = mongoDB.DatabaseName ?? ""
    },
    IdentityOptionsAction = options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireLowercase = false;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        options.Lockout.MaxFailedAccessAttempts = 5;

        options.User.RequireUniqueEmail = true;


    }
};

builder.Services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIndenityConfig)
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddDefaultTokenProviders();

// JWT Confi
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionSetup>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();

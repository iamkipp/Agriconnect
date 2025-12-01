using Marketplace.Infrastructure;
using Marketplace.Application.Interfaces;
using Marketplace.Application.Services;
using Marketplace.Infrastructure.Payments;
using Marketplace.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Marketplace.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// --- Configuration ---
var configuration = builder.Configuration;
configuration.AddEnvironmentVariables();

// --- Add services ---
builder.Services.AddControllers();

// Add OpenAPI (Scalar)
builder.Services.AddOpenApi(options =>
{
	options.AddDocument("v1", doc =>
	{
		doc.Title = "Agriculture Marketplace API";
		doc.Version = "v1";
		doc.Description = "Kenya-focused marketplace: escrow, premier boosts, delivery confirmations. Use /scalar for UI.";
	});
});

// DbContext
var connection = builder.Configuration.GetConnectionString("DefaultConnection")
				 ?? builder.Configuration["DB__CONNECTION"]
				 ?? "Host=localhost;Port=5432;Database=marketplace;Username=marketplace;Password=marketplacepwd";
builder.Services.AddDbContext<MarketplaceDbContext>(opts =>
	opts.UseNpgsql(connection));

// Application services & DI
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEscrowService, EscrowService>();
builder.Services.AddScoped<IPremierService, PremierService>();
builder.Services.AddScoped<IUserService, UserService>();

// Payment provider stub (replace with MpesaPaymentProvider later)
builder.Services.AddSingleton<IPaymentProvider, InMemoryPaymentProvider>();

// Repositories (infrastructure)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Hosted services (background workers)
builder.Services.AddHostedService<EscrowReleaseWorker>();
builder.Services.AddHostedService<PremierExpiryWorker>();

// JWT Auth (simple symmetric key)
var jwtKey = builder.Configuration["JWT__KEY"] ?? "very_secret_dev_key_change_in_prod";
var issuer = builder.Configuration["JWT__ISSUER"] ?? "marketplace";
var audience = builder.Configuration["JWT__AUDIENCE"] ?? "marketplace_users";
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = true,
		ValidIssuer = issuer,
		ValidateAudience = true,
		ValidAudience = audience,
		ValidateLifetime = true
	};
});

// Map OpenAPI + Scalar endpoints
var app = builder.Build();

app.UseRouting();

// OpenAPI endpoints
app.MapOpenApi();

// Use Scalar UI at /scalar
app.UseScalar(options =>
{
	options.Title = "Agriculture Marketplace API";
	options.Theme = ScalarTheme.Default;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// apply migrations at startup in dev
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<MarketplaceDbContext>();
	db.Database.Migrate();
}

app.Run();

using Microsoft.EntityFrameworkCore;
using OnlineStore.API.Exceptions;
using OnlineStore.API.Extensions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Services;
using OnlineStore.Infrastructure.Persistence;
using OnlineStore.Infrastructure.Persistence.Repositories;
using OnlineStore.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── Swagger / OpenAPI ─────────────────────────────────────────────────────────
builder.Services.AddOnlineStoreSwagger();

// ── Tratamento global de exceções (RFC 7807 ProblemDetails) ───────────────────
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// ── Health Checks ─────────────────────────────────────────────────────────────
builder.Services.AddOnlineStoreHealthChecks();

// ── Banco de dados — Oracle + EF Core ─────────────────────────────────────────
builder.Services.AddDbContext<OnlineStoreContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("OnlineStoreContextOracle");
    options.UseOracle(connectionString);
});

// ── Repositório Genérico ──────────────────────────────────────────────────────
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// ── Serviços ──────────────────────────────────────────────────────────────────
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

// ─────────────────────────────────────────────────────────────────────────────
var app = builder.Build();

// ── Exception Handler DEVE vir antes dos Controllers ─────────────────────────
app.UseExceptionHandler();

// ── Swagger UI (apenas em ambiente de desenvolvimento) ────────────────────────
if (app.Environment.IsDevelopment())
    app.UseOnlineStoreSwagger();

app.UseOnlineStoreHealthChecks(app.Environment);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

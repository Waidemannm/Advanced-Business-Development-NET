using Microsoft.EntityFrameworkCore;
using OnlineStore.API.Exceptions;
using OnlineStore.API.Extensions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Infrastructure.Persistence;
using OnlineStore.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── Swagger / OpenAPI ─────────────────────────────────────────────────────────
builder.Services.AddOnlineStoreSwagger();

// ── Tratamento global de exceções (RFC 7807 ProblemDetails) ───────────────────
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// ── Banco de dados — Oracle + EF Core ─────────────────────────────────────────
builder.Services.AddDbContext<OnlineStoreContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("OnlineStoreContextOracle");
    options.UseOracle(connectionString);
});

// ── Repositório Genérico ──────────────────────────────────────────────────────
// Registro como open generic — resolve IRepository<T> para qualquer T : BaseEntity
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// ── Serviços de Aplicação ─────────────────────────────────────────────────────
builder.Services.AddScoped<IAddressService,       AddressService>();
builder.Services.AddScoped<ICategoryService,      CategoryService>();
builder.Services.AddScoped<IProductService,       ProductService>();

// ─────────────────────────────────────────────────────────────────────────────
var app = builder.Build();

// ── Exception Handler DEVE vir antes dos Controllers ─────────────────────────
app.UseExceptionHandler();

// ── Swagger UI (apenas em ambiente de desenvolvimento) ────────────────────────
if (app.Environment.IsDevelopment())
    app.UseOnlineStoreSwagger();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

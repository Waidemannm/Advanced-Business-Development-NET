# OnlineStore — CP3 🛒

## 👥 Integrantes do Grupo

| Nome | RM |
|---|---|
| Thiago Rodrigues da Mota | RM 563650 |
| Moisés Waidemann Molinillo Júnior | RM 563719 |
| Gabriel Sbrana Campos | RM 565849 |

**Turma:** 2TDSPF — 2026

---

## 📦 Domínio

**OnlineStore** — sistema de e-commerce com gestão de:
- **Clientes** (Costumer) com validação de idade mínima (13 anos) e unicidade de e-mail
- **Endereços** (Address) associados aos clientes
- **Pagamentos** (Payment) com suporte a Crédito, Débito, Pix e Outros
- **Categorias** (Category) para organização dos produtos
- **Produtos** (Product) com nome, descrição, preço e estoque
- **Avaliações** (RatingProduct) — cada cliente avalia cada produto uma única vez

---

## 🏛️ Arquitetura

Clean Architecture com 4 projetos:

```
OnlineStore.Domain          → Entidades, Enums, Exceções de domínio
OnlineStore.Application     → Interfaces, Serviços, DTOs, IRepository<T>
OnlineStore.Infrastructure  → DbContext, Configurações Fluent API, Repository<T>
OnlineStore.API             → Controllers, Program.cs, Swagger, GlobalExceptionHandler
```

### Fluxo de dependências

```
API → Application → Domain
API → Infrastructure → Application → Domain
```

> A camada **API** nunca acessa `DbContext` diretamente — usa apenas `IRepository<T>` e `IXxxService`.

---

## 🗄️ SGBD

**Oracle Database** via `Oracle.EntityFrameworkCore` v9.23.60 + Entity Framework Core 9.

As tabelas herdam os nomes do CP2 (`T_CP1_*`) para manter compatibilidade com as migrations existentes.

---

## 🚀 Como executar a API

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Oracle Database (local ou remoto)

### 1. Configurar connection string

Edite `OnlineStore.API/appsettings.Development.json` com suas credenciais Oracle:

```json
{
  "ConnectionStrings": {
    "OnlineStoreContextOracle": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/XEPDB1"
  }
}
```

> ⚠️ **Não commite credenciais reais.** Use variáveis de ambiente ou User Secrets em produção.

### 2. Executar as migrations (se necessário)

```bash
cd Cps/OnlineStore-Cp3
dotnet ef database update --project OnlineStore.Infrastructure --startup-project OnlineStore.API
```

### 3. Rodar a API

```bash
cd Cps/OnlineStore-Cp3/OnlineStore.API
dotnet run
```

### 4. Acessar o Swagger UI

Abra o navegador em:

```
https://localhost:7001/swagger
```

ou

```
http://localhost:5001/swagger
```

---

## 📚 Repositório Genérico (`IRepository<T>`)

### Contrato (`OnlineStore.Application/Interfaces/IRepository.cs`)

```csharp
public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    void Delete(T entity);
    Task<bool> ExistsByIdAsync(Guid id);
    Task SaveChangesAsync();
}
```

### Implementação (`OnlineStore.Infrastructure/Persistence/Repositories/Repository.cs`)

Usa `DbContext.Set<T>()` e `AsNoTracking()` em leituras para performance otimizada.

### Registro na DI (`Program.cs`)

```csharp
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
```

### Uso demonstrado

O **`CategoryController`** utiliza `IRepository<Category>` diretamente (sem serviço intermediário), demonstrando o uso consistente do repositório genérico em todos os endpoints de Category (GET, POST, PUT, DELETE).

---

## ⚠️ Tratamento Global de Erros (`GlobalExceptionHandler`)

Implementa `IExceptionHandler` e retorna respostas no padrão **RFC 7807** (`application/problem+json`).

### Mapeamento de exceções → HTTP

| Exceção | Status HTTP |
|---|---|
| `ArgumentException` / `ArgumentNullException` | 400 Bad Request |
| `InvalidOperationException` | 400 Bad Request |
| `DomainException` | 400 Bad Request |
| `ResourceNotFoundException` | 404 Not Found |
| `KeyNotFoundException` | 404 Not Found |
| Qualquer outra | 500 Internal Server Error |

### Exemplo de resposta de erro (404)

```json
{
  "type": "https://httpstatuses.com/404",
  "title": "Recurso não encontrado",
  "status": 404,
  "detail": "Categoria com id '3fa85f64-5717-4562-b3fc-2c963f66afa6' não encontrada.",
  "instance": "/api/Category/3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> **Produção:** stack trace nunca é exposto. Erros 500 retornam mensagem genérica.

---

## 🔗 Endpoints disponíveis

| Recurso | GET all | GET by id | POST | PUT | DELETE |
|---|---|---|---|---|---|
| `/api/Address` | ✅ | ✅ | ✅ | ✅ | ✅ |
| `/api/Category` | ✅ | ✅ | ✅ | ✅ | ✅ |
| `/api/Product` | ✅ | ✅ | ✅ | ✅ | ✅ |

---

## 🔄 Relação com os CPs anteriores

| CP | Foco |
|---|---|
| CP1 | MER + Entidades C# sem banco |
| CP2 | Schema físico + EF Core + Oracle + Migrations |
| CP3 | **API REST** + Swagger completo + `IRepository<T>` + `GlobalExceptionHandler` |

## 🩺 Health Checks e Observabilidade (CP4)

**Health Checks**: A API conta com endpoints de Health Check registrados via `Microsoft.Extensions.Diagnostics.HealthChecks`.
- **`GET /health`**: Retorna um JSON contendo o status geral (`Healthy`, `Degraded` ou `Unhealthy`) e a verificação do Banco de Dados Oracle.

**Observabilidade (Logs)**: 
- Foram implementados logs estruturados com `ILogger` nos controllers. O log é gerado no início e no final (sucesso/falha) das requisições (ex: em `ProductController.Create`).
- Cada log carrega o `traceId` (`HttpContext.TraceIdentifier`) para facilitar correlações.
- O `GlobalExceptionHandler` também loga o `traceId` em nível de erro.
- Em desenvolvimento, o `traceId` é retornado na extensão da resposta do `ProblemDetails`.

## 🧪 Testes Automatizados (xUnit - CP4)

O projeto contém testes automatizados (base da pirâmide e integração de serviços) utilizando **xUnit** e **Moq**.
- **Domain.Tests**: Testes que garantem regras de domínio puras (ex: validação da Entidade Category). (Utilizando os padrões Arrange/Act/Assert, `[Fact]` e `[Theory]`).
- **Application.Tests**: Testes sobre `ProductService` utilizando `Moq` para isolar o Repositório, garantindo as regras de negócio em cenários de exceção e sucesso.

Para rodar todos os testes, execute o seguinte comando na raiz da Solução:
```bash
dotnet test
```

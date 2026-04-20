# CheckPoint 2 — Online Store (2TDSPF)

Continuação do CP1, com persistência real via Entity Framework Core, mapeamento Fluent API e Clean Architecture.

## Integrantes

| Nome | RM |
|---|---|
| Thiago Rodrigues da Mota | 563650 |
| Moisés Waidemann Molinillo Júnior | 563719 |
| Gabriel Sbrana Campos | 565849 |

---

## Domínio Escolhido

Sistema de e-commerce onde o cliente pode visualizar produtos, escolher a forma de pagamento e avaliar os itens adquiridos. O modelo cobre as entidades de **Cliente**, **Endereço**, **Pagamento**, **Produto**, **Categoria** e **Avaliação de Produto**.

---

## SGBD Utilizado

**Oracle Database**
Provider: `Oracle.EntityFrameworkCore` v9.23.60

---

## Como executar

### Pré-requisitos

- .NET 10 SDK
- Acesso a uma instância Oracle (local ou remota)
- EF Core CLI: `dotnet tool install --global dotnet-ef`

### Configuração da connection string

No arquivo `OnlineStore.API/appsettings.json`, preencha com os dados da sua instância Oracle:

```json
{
  "ConnectionStrings": {
    "OnlineStoreContextOracle": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=SEU_HOST:1521/SEU_SERVICE"
  }
}
```

> ⚠️ Nunca commite credenciais reais. Use variáveis de ambiente ou secrets em produção.

### Aplicar a migration e criar o banco

```bash
cd OnlineStore.API
dotnet ef database update --project ../OnlineStore.Infrastructure
```

### Rodar a aplicação

```bash
cd OnlineStore.API
dotnet run
```

A API estará disponível em `https://localhost:7000` (ou conforme `launchSettings.json`).

---

## Arquitetura

O projeto segue **Clean Architecture** dividido em 4 camadas:

```
OnlineStore.Domain          → Entidades, Enums, BaseEntity
OnlineStore.Application     → Interfaces, Services, DTOs
OnlineStore.Infrastructure  → DbContext, Configurations, Migrations
OnlineStore.API             → Controllers, Program.cs
```

A camada de Infrastructure **não contém regras de negócio** — apenas persistência, mapeamento e acesso a dados.

---

## Entidades e Relacionamentos

### Entidades

| Entidade | Tabela | Atributos principais |
|---|---|---|
| Address | T_CP1_ADRESS | Street, City, State, PostalCode, Number, Country |
| Payment | T_CP1_PAYMENT | Value, PaymentWay |
| Category | T_CP1_CATEGORY | Name, Description |
| Product | T_CP1_PRODUCT | IdCategory, Name, Description, Price, Stock |
| Costumer | T_CP1_COSTUMER | IdPayment, IdAddress, Name, BirthDate, Gender, Email |
| RatingProduct | T_CP1_RATING_PRODUCT | IdCostumer, IdProduct, Score |

### Relacionamentos

| Relacionamento | Cardinalidade | Descrição |
|---|---|---|
| Costumer → Address | 1:1 | Um cliente possui um endereço |
| Costumer → Payment | N:1 | Vários clientes podem usar a mesma forma de pagamento |
| Product → Category | N:1 | Um produto pertence a uma categoria |
| Costumer → RatingProduct | 1:N | Um cliente pode avaliar vários produtos |
| Product → RatingProduct | 1:N | Um produto pode receber várias avaliações |

---

## Mapeamento (Fluent API)

Todas as entidades possuem `IEntityTypeConfiguration<T>` em `OnlineStore.Infrastructure/Persistence/Configurations/`:

- Chaves primárias e nomes de colunas explícitos
- Tamanhos máximos (`HasMaxLength`) alinhados com as validações do Domain
- Precisão decimal (`HasPrecision`) para `Value` e `Price`
- Índices únicos em `Category.Name`, `Product.Name` e `Costumer.Name`
- `RatingProduct` com **chave primária composta** `{IdCostumer, IdProduct}`, sem herdar `BaseEntity`
- Relacionamentos com `OnDelete(DeleteBehavior.Cascade)`

---

## Migrations

| Migration | Data | Descrição |
|---|---|---|
| `20260420022424_Initial` | 20/04/2026 | Criação completa do esquema (todas as tabelas, FKs e índices) |

Para visualizar o esquema gerado:

```bash
dotnet ef migrations script --project OnlineStore.Infrastructure --startup-project OnlineStore.API
```

---

## Injeção de Dependência

Registrado em `Program.cs`:

```csharp
builder.Services.AddDbContext<OnlineStoreContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OnlineStoreContextOracle")));

builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICostumerService, CostumerService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRatingProductService, RatingProductService>();
```

---

## Endpoints disponíveis

Cada entidade possui controller com os seguintes endpoints:

| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/{entidade}` | Criar |
| GET | `/api/{entidade}` | Listar todos |
| GET | `/api/{entidade}/{id}` | Buscar por Id |
| PUT | `/api/{entidade}/{id}` | Atualizar |
| DELETE | `/api/{entidade}/{id}` | Remover |

`RatingProduct` usa rota composta: `/api/ratingproduct/{idCostumer}/{idProduct}`

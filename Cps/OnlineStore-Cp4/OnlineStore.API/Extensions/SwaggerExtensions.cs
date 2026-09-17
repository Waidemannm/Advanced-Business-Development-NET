using System.Reflection;
using Microsoft.OpenApi.Models;

namespace OnlineStore.API.Extensions;

/// <summary>
/// Extensões para configuração do Swagger/OpenAPI na DI e no pipeline.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Registra e configura o Swagger com metadados do domínio e comentários XML.
    /// </summary>
    public static IServiceCollection AddOnlineStoreSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title       = "OnlineStore API",
                Version     = "v1",
                Description = """
                    API REST do sistema de e-commerce OnlineStore (CP3 — FIAP 2TDSPF 2026).

                    Permite gerenciar Clientes, Endereços, Pagamentos, Produtos,
                    Categorias e Avaliações de Produto sobre banco de dados Oracle.

                    Integrantes:
                    - Thiago Rodrigues da Mota — RM 563650
                    - Moisés Waidemann Molinillo Júnior — RM 563719
                    - Gabriel Sbrana Campos — RM 565849
                    """,
                Contact = new OpenApiContact
                {
                    Name  = "Grupo — Thiago, Moisés, Gabriel (2TDSPF)",
                    Email = "grupo@fiap.com.br"
                }
            });

            // Inclui comentários XML gerados automaticamente pelo projeto API
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    /// <summary>
    /// Habilita o middleware de Swagger UI no pipeline de requisições.
    /// </summary>
    public static IApplicationBuilder UseOnlineStoreSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(ui =>
        {
            ui.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineStore API v1");
            ui.RoutePrefix = "swagger";
            ui.DocumentTitle = "OnlineStore API — CP3";
        });
        return app;
    }
}

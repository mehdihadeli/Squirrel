using Squirrel.Core.Extensions.ServiceCollectionExtensions;
using Squirrel.OpenApi.AspnetOpenApi.Transformers;
using Microsoft.Extensions.Hosting;

namespace Squirrel.OpenApi.AspnetOpenApi.Extensions;

// https://github.com/dotnet/aspnet-api-versioning/issues/1115
public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddAspnetOpenApi(this IHostApplicationBuilder builder, string[] versions)
    {
        builder.Services.AddConfigurationOptions<OpenApiOptions>();

        foreach (var documentName in versions)
        {
            builder.Services.AddOpenApi(
                documentName,
                options =>
                {
                    options.AddDocumentTransformer<OpenApiVersioningDocumentTransformer>();
                    options.AddOperationTransformer<AuthorizationChecksTransformers>();
                    options.AddOperationTransformer<OperationDeprecatedStatusTransformers>();
                    options.AddDocumentTransformer<SecuritySchemeDocumentTransformer>();
                    options.AddOperationTransformer<OpenApiDefaultValuesOperationTransformer>();
                    options.AddSchemaTransformer<SchemaNullableFalseTransformers>();
                    options.AddSchemaTransformer<EnumSchemaTransformer>();
                }
            );
        }

        return builder;
    }
}

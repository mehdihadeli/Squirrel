using Aspire.Hosting.ApplicationModel;

namespace Squirrel.AspireIntegrations.Swagger;

public class SwaggerUIAnnotation(string[] documentNames, string path, EndpointReference endpointReference)
    : IResourceAnnotation
{
    public string[] DocumentNames { get; } = documentNames;
    public string Path { get; } = path;
    public EndpointReference EndpointReference { get; } = endpointReference;
}

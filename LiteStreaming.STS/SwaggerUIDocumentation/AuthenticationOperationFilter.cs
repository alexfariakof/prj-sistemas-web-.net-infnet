using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LiteStreaming.STS.SwaggerUIDocumentation;
public class AuthenticationOperationFilter : IDocumentFilter
{
    public AuthenticationOperationFilter() { }
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var operation = new OpenApiOperation
        {
            Summary = "Autenticação do cliente",
            Description = "Endpoint personalizado para autenticação do cliente.",
            RequestBody = new OpenApiRequestBody
            {
                Required = true,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/x-www-form-urlencoded"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Required = new HashSet<string> { "client_id", "client_secret", "grant_type", "username", "password", "scope" },
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["client_id"] = new OpenApiSchema {  Type = "string", Description = "ID do cliente", Nullable = false, Default = new OpenApiString("")  },
                                ["client_secret"] = new OpenApiSchema { Type = "string", Description = "Segredo do cliente", Nullable = false, Default = new OpenApiString("") },
                                ["grant_type"] = new OpenApiSchema { Type = "string", Description = "Tipo de concessão", Nullable = false, Default = new OpenApiString("") },
                                ["username"] = new OpenApiSchema { Type = "string", Description = "Nome de usuário", Nullable = false, Default = new OpenApiString("") },
                                ["password"] = new OpenApiSchema { Type = "string", Description = "Senha do usuário", Nullable = false, Default = new OpenApiString("") },
                                ["scope"] = new OpenApiSchema { Type = "string", Description = "Escopo", Nullable = false, Default = new OpenApiString("") }
                            }
                        }
                    }
                }
            },
            Responses = new OpenApiResponses
            {
                ["200"] = new OpenApiResponse
                {
                    Description = "Sucesso",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["access_token"] = new OpenApiSchema { Type = "string", Description = "Token de acesso" },
                                    ["expires_in"] = new OpenApiSchema { Type = "integer", Description = "Tempo de expiração do token em segundos" },
                                    ["token_type"] = new OpenApiSchema { Type = "string", Description = "Tipo de token" },
                                    ["scope"] = new OpenApiSchema { Type = "string", Description = "Escopo" }
                                }
                            },
                            Examples = new Dictionary<string, OpenApiExample>
                            {
                                ["example1"] = new OpenApiExample
                                {
                                    Summary = "Exemplo de resposta de sucesso",
                                    Value = new OpenApiObject
                                    {
                                        ["access_token"] = new OpenApiString("string"),
                                        ["expires_in"] = new OpenApiInteger(0),
                                        ["token_type"] = new OpenApiString("string"),
                                        ["scope"] = new OpenApiString("string")
                                    }
                                }
                            }
                        }
                    }
                },
                ["400"] = new OpenApiResponse
                {
                    Description = "Erro de solicitação inválida",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["error"] = new OpenApiSchema { Type = "string", Description = "Tipo de erro" },
                                    ["error_description"] = new OpenApiSchema { Type = "string", Description = "Descrição do erro" }
                                }
                            },
                            Examples = new Dictionary<string, OpenApiExample>
                            {
                                ["example1"] = new OpenApiExample
                                {
                                    Summary = "Exemplo de resposta de erro",
                                    Value = new OpenApiObject
                                    {
                                        ["error"] = new OpenApiString("string"),
                                        ["error_description"] = new OpenApiString("string")
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
        swaggerDoc.Paths.Add("/connect/token", new OpenApiPathItem
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Post] = operation
            }            
        });
    }
}


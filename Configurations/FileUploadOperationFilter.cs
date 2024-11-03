﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
namespace NhaSachDaiThang_BE_API.Configurations
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(List<IFormFile>))
                .ToArray();

            if (fileParams.Length == 0)
                return;

            operation.RequestBody = new OpenApiRequestBody
            {
                Content = {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = fileParams.ToDictionary(
                            p => p.Name,
                            p => new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary"
                            }
                        ),
                        Required = fileParams.Select(p => p.Name).ToHashSet() // Chuyển thành IList<string>
                    }
                }
            }
            };
        }
    }
}

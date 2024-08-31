using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RollsApi.Extensions
{
  public class SwaggerCustomHeader : IOperationFilter
  {
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      if (operation.Parameters is null)
      {
        operation.Parameters = new List<OpenApiParameter>();
      }

      operation.Parameters.Add(new OpenApiParameter
      {
        Name = "Api-Key",
        In = ParameterLocation.Header,
        Description = "Enter Your API Key",
        Required = true,
      });
    }
  }
}

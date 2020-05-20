using System.Collections.Generic;
using Helpdesk.WebAPI.Areas;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Helpdesk.WebAPI.Docs
{
    public class TagDescriptionDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = AreaNames.Tickets, Description = "Resource endpoints for managing all the Tickets." }
            };
        }
    }
}
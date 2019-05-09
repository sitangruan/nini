using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace nini.Filters
{
    public class TagByApiExplorerSettingsOperationFilter: IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiGroupNames = context
                .ApiDescription
                .ControllerAttributes()
                .OfType<ApiExplorerSettingsAttribute>()
                .Where(x => !x.IgnoreApi)
                .Select(x => x.GroupName)
                .ToList();

            if (apiGroupNames.Count == 0)
                return;

            var tags = operation.Tags?.Select(x => x).ToList() ?? new List<string>();

            //r controllerDescriptor = context.ApiDescription.GetProperty<ControllerActionDescriptor>();
            var controllerDescriptor = (ControllerActionDescriptor)context.ApiDescription.ActionDescriptor;
            if (controllerDescriptor != null)
                tags.Remove(controllerDescriptor.ControllerName);

            foreach (var apiGroupName in apiGroupNames)
                if (!tags.Contains(apiGroupName))
                    tags.Add(apiGroupName);

            operation.Tags = tags;
        }
    }
}

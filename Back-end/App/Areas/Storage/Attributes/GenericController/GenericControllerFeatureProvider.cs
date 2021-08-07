using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App.Areas.Storage.Attributes.GenericController
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var genericControllers = GetType().Assembly.GetTypes()
                .Where(type => type.BaseType == typeof(Controller)
                    && type.IsGenericType);

            foreach (var controller in genericControllers)
            {   
                if (controller.GetCustomAttributes()
                    .Any(attribute => attribute.GetType() == typeof(GenericControllerAttribute)))
                {
                    var genericControllerBaseType = controller
                     .GetGenericArguments()
                     .First()
                     .BaseType;

                    var genericControllerTypes = GetType().Assembly.GetTypes()
                        .Where(type => type.IsSubclassOf(genericControllerBaseType)
                            && type.IsAbstract == false)
                        .ToArray();

                    foreach (var genericType in genericControllerTypes)
                    {
                        var controllerTypeInfo = controller.GetGenericTypeDefinition()
                            .MakeGenericType(genericType)
                            .GetTypeInfo();

                        feature.Controllers.Add(controllerTypeInfo);
                    }
                }  
            }
        }
    }
}

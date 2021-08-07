using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.Attributes.GenericController
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GenericControllerAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var entityType = controller.ControllerType.GenericTypeArguments[0];
            controller.ControllerName = entityType?.Name;
        }
    }
}

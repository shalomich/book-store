using App.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Attributes.GenericController
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GenericControllerAttribute : Attribute, IControllerModelConvention
    { 
        public void Apply(ControllerModel controller)
        {
            string controllerName = controller.ControllerType.GenericTypeArguments[0].Name;
            
            controller.ControllerName = controllerName;
        }
    }
}

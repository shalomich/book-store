using App.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Areas.Storage.Attributes.GenericController
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GenericControllerAttribute : Attribute, IControllerModelConvention
    { 
        public void Apply(ControllerModel controller)
        {
            string viewModelName = controller.ControllerType.GenericTypeArguments[0].Name;
            string resourceName = Regex.Match(viewModelName, @"^[A-z][a-z]+").Value;

            controller.ControllerName = resourceName;
        }
    }
}

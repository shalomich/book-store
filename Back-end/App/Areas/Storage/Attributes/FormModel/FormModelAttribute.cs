﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.Attributes.FormModel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)] 
    public class FormModelAttribute : Attribute 
    {
    }
}

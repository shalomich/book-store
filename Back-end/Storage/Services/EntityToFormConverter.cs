﻿using Microsoft.Extensions.Configuration;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Storage.Services
{
    public class EntityToFormConverter
    {
        private class PropertyToFormElement
        {
            private string _typeName;
            private string _defaultFormElement;
            public string TypeName {
                set 
                {
                    try
                    {
                        Type.GetType(value);
                        _typeName = value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException(); 
                    }
                } 
                get 
                {
                    return _typeName;
                } 
            }        
            public string[] FormElements { set; get; }
            public string DefaultFormElement { 
                set 
                {
                    if (FormElements.Contains(value) == false)
                        throw new ArgumentOutOfRangeException();
                    _defaultFormElement = value;
                } 
                get 
                {
                    return _defaultFormElement;
                } 
            }
        }
        
        private const string _entitiesSectionName = "form:entity";
        private const string _settingsSectionName = "form:settings";
        
        private readonly IEnumerable<PropertyToFormElement> _defaultSettings;
        private IConfigurationSection _entitiesSection;
        public EntityToFormConverter(IConfiguration configuration) 
        {
            
            _defaultSettings = configuration
                .GetSection(_settingsSectionName)
                .Get<PropertyToFormElement[]>();

            _entitiesSection = configuration.GetSection(_entitiesSectionName);                                         
        }

        public Dictionary<string,string> Convert<T>() where T : Entity
        {
            Type convertedType = typeof(T);
            PropertyInfo[] properties = convertedType.GetProperties();

            IConfigurationSection currentEntitySection = _entitiesSection.GetSection(convertedType.Name);
            Dictionary<string, string> entitySettings = currentEntitySection
                                                            .GetChildren()
                                                            .ToDictionary(
                                                                section => section.Key,
                                                                section => section.Value);
            
            var finalSettings = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                PropertyToFormElement currentElement = _defaultSettings.FirstOrDefault(currentElement => 
                                                Type.GetType(currentElement.TypeName) == property.PropertyType);
                if (currentElement == null)
                    continue;

                string currentFormElement = currentElement.DefaultFormElement;

                if (entitySettings.TryGetValue(property.Name, out string formElement) == true)
                    if (currentElement.FormElements.Contains(formElement) == true)
                        currentFormElement = formElement;
                
                finalSettings.Add(property.Name, currentFormElement);
            }

            return finalSettings;
        }
    }
}

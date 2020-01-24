using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace SujaySarma.Sdk.Azure.Core.Common
{
    /// <summary>
    /// An "expando" class that can be used as the base for various "properties" structures 
    /// through almost all Azure API request+result objects
    /// </summary>
    public class AzureObjectPropertiesBase<T> : DynamicObject, IDynamicMetaObjectProvider 
        where T : class
    {

        /// <summary>
        /// Gets/sets the property
        /// </summary>
        /// <param name="propertyName">Exact name of the property</param>
        /// <returns>Value of the property. If property was not found, returns NULL</returns>
        public object? this[string propertyName]
        {
            get
            {
                if (!_instanceProperties.TryGetValue(propertyName, out PropertyInfo? property))
                {
                    // try the expando set
                    if (_expandoProperties.TryGetValue(propertyName, out object? value))
                    {
                        return value;
                    }
                }

                if (property != null)
                {
                    return property.GetValue(_instance);
                }

                return null;
            }
            set
            {
                if (_instanceProperties.TryGetValue(propertyName, out PropertyInfo? property))
                {
                    if (property != null)
                    {
                        property.SetValue(_instance, value);
                    }
                }

                _expandoProperties[propertyName] = value;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (!_instanceProperties.TryGetValue(binder.Name, out PropertyInfo? property))
            {
                // try the expando set
                if (_expandoProperties.TryGetValue(binder.Name, out object? value))
                {
                    result = value;
                    return true;
                }                
            }

            if (property != null)
            {
                result = property.GetValue(_instance);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_instanceProperties.TryGetValue(binder.Name, out PropertyInfo? property))
            {
                if (property != null)
                {
                    property.SetValue(_instance, value);
                    return true;
                }
            }

            _expandoProperties[binder.Name] = value;
            return true;
        }


        public AzureObjectPropertiesBase(T instance)
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(instance));
            _expandoProperties = new Dictionary<string, object?>();

            _instanceProperties = new Dictionary<string, PropertyInfo>();
            foreach(PropertyInfo property in instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                _instanceProperties.Add(property.Name, property);
            }
        }

        private T _instance;
        private Dictionary<string, PropertyInfo> _instanceProperties;
        private Dictionary<string, object?> _expandoProperties;
    }
}

using System;
using System.Linq;
using System.Reflection;

using BP.SDK.Interfaces.Common;

namespace BP.SDK.Common
{
    /// <summary>
    /// Implements Property Utils
    /// 
    /// 2017/08/03 - Created, VTyagunov
    /// </summary>
    public class PropertyUtils : IPropertyUtils
    {
        #region Methods

        /// <summary>
        /// Use for sort of object property names
        /// </summary>
        /// <param name="obj">Object for sort a properties</param>
        /// <returns></returns>
        public IOrderedEnumerable<PropertyInfo> GetOrderedProperties(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("GetOrderedProperties");

            try
            {
                var type = obj.GetType();
                var unOrderedProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var orderedProperties =
                    type.GetProperties().OrderBy(item => item.Name);

                return orderedProperties;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Use for Return property value
        /// </summary>
        /// <param name="obj">Object for return of property value</param>
        /// <param name="propertyName">Property name</param>
        /// <returns></returns>
        public object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException("GetPropertyValue");

            try
            {
                var propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public |
                    BindingFlags.Instance);
                if (propertyInfo == null)
                    throw new ArgumentException(string.Format(
                        "GetPropertyValue. Property '{0}' is not found!", propertyInfo), "property");

                return propertyInfo.GetValue(obj, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Use for Set property value
        /// </summary>
        /// <param name="obj">Object for return of property value</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="newValue">New value for property</param>
        public void SetPropertyValue(object obj, string propertyName, object newValue)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (newValue == null)
                throw new ArgumentNullException("newValue");

            try
            {
                var propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public |
                    BindingFlags.Instance);
                if (propertyInfo == null)
                    throw new ArgumentException(string.Format(
                        "GetPropertyValue. Property '{0}' is not found!", propertyInfo), "property");

                propertyInfo.SetValue(obj, newValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
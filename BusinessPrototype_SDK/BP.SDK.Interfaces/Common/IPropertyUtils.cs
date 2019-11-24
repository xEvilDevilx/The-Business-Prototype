using System.Linq;
using System.Reflection;

namespace BP.SDK.Interfaces.Common
{
    /// <summary>
    /// Presents Property Utils
    /// 
    /// 2017/08/03 - Created, VTyagunov
    /// </summary>
    public interface IPropertyUtils
    {
        /// <summary>
        /// Use for sort of object property names
        /// </summary>
        /// <param name="obj">Object for sort a properties</param>
        /// <returns></returns>
        IOrderedEnumerable<PropertyInfo> GetOrderedProperties(object obj);

        /// <summary>
        /// Use for Return property value
        /// </summary>
        /// <param name="obj">Object for return of property value</param>
        /// <param name="propertyName">Property name</param>
        /// <returns></returns>
        object GetPropertyValue(object obj, string propertyName);

        /// <summary>
        /// Use for Set property value
        /// </summary>
        /// <param name="obj">Object for return of property value</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="newValue">New value for property</param>
        void SetPropertyValue(object obj, string propertyName, object newValue);
    }
}
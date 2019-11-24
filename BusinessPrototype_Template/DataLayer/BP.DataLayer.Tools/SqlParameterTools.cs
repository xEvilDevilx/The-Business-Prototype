using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security;

using BP.DataLayer.Base.Attributes;
using BP.DataLayer.Base.Enums;
using BP.SDK.Extensions;
using BP.SDK.Log;

namespace BP.DataLayer.Tools
{
    /// <summary>
    /// Presents SqlParameter Tools functionality
    /// 
    /// 2018/11/04 - Created, VTyagunov
    /// </summary>
    public static class SqlParameterTools
    {
        /// <summary>
        /// Use for Create SqlParameters array
        /// </summary>
        /// <typeparam name="TObject">Type of the object for create sql parameters</typeparam>
        /// <param name="tObj">Object of the TObject type</param>
        /// <param name="operationKey">Business object property Operation Key</param>
        /// <returns></returns>
        public static SqlParameter[] CreateSqlParameters<TObject>(TObject tObj, OperationKeyTypes operationKey)
        {
            Logger.Log.Debug("SqlParameterTools. CreateSqlParameters");

            if (tObj == null)
                throw new ArgumentNullException(nameof(tObj));

            try
            {
                Type type = tObj.GetType();
                PropertyInfo[] allProperties = type.GetProperties();
                PropertyInfo[] properties = allProperties.Where(x => Attribute.IsDefined(x, typeof(OperationsAttribute), false)).ToArray();
                var sqlParameters = new List<SqlParameter>();
                foreach (var property in properties)
                {
                    var operationsAttr = property.GetCustomAttribute<OperationsAttribute>();
                    if (operationsAttr == null || !operationsAttr.OperationKeys.HasFlag(operationKey))
                        continue;

                    var parametersAttr = property.GetCustomAttribute<ParametersAttribute>();
                    var propertyValue = property.GetValue(tObj);
                    var sqlParameter = CreateSqlParameter(parametersAttr, propertyValue);
                    if (sqlParameter != null)
                        sqlParameters.Add(sqlParameter);
                }

                return sqlParameters.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Create SqlParameters array
        /// </summary>
        /// <typeparam name="TObject">Type of the object for create sql parameters</typeparam>
        /// <param name="tObj">Object of the TObject type</param>
        /// <param name="operationKey">Business object property Operation Key</param>
        /// <returns></returns>
        public static SqlParameter[] CreateSqlParameters<TObject, TPropertiesEnum>(TObject tObj, TPropertiesEnum tPropertiesEnumType)
        {
            Logger.Log.Debug("SqlParameterTools. CreateSqlParameters");

            if (tObj == null)
                throw new ArgumentNullException(nameof(tObj));

            if (!typeof(TPropertiesEnum).IsEnum)
                throw new ArgumentException("TPropertiesEnum must be an enumerated type");

            try
            {
                Type type = tObj.GetType();
                PropertyInfo[] allProperties = type.GetProperties();
                PropertyInfo[] properties = allProperties.Where(x => Attribute.IsDefined(x, typeof(OperationsEnumAttribute), false)).ToArray();
                var sqlParameters = new List<SqlParameter>();
                foreach (var property in properties)
                {
                    var operationsAttr = property.GetCustomAttribute<OperationsEnumAttribute>();
                    if (operationsAttr == null || !operationsAttr.OperationKeys.HasFlag(tPropertiesEnumType as Enum))
                        continue;

                    var parametersAttr = property.GetCustomAttribute<ParametersAttribute>();
                    var propertyValue = property.GetValue(tObj);
                    var sqlParameter = CreateSqlParameter(parametersAttr, propertyValue);
                    if (sqlParameter != null)
                        sqlParameters.Add(sqlParameter);
                }

                return sqlParameters.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Create SqlParameter object
        /// </summary>
        /// <typeparam name="T">Type of the object with value for set as SqlParameter Value</typeparam>
        /// <param name="parametersAttr">Object of the ParametersAttribute</param>
        /// <param name="valueForSet">Object with value for set as SqlParameter Value</param>
        /// <returns></returns>
        private static SqlParameter CreateSqlParameter<T>(ParametersAttribute parametersAttr, T valueForSet)
        {
            Logger.Log.Debug("SqlParameterTools. CreateSqlParameter");

            if (parametersAttr == null)
                throw new ArgumentNullException(nameof(parametersAttr));

            try
            {
                var sqlParameter = new SqlParameter(parametersAttr.Name, parametersAttr.Type);
                sqlParameter.Direction = parametersAttr.Direction;
                if (valueForSet == null)
                {
                    sqlParameter.Value = DBNull.Value;
                    return sqlParameter;
                }

                var tType = valueForSet.GetType();
                if (tType == typeof(SecureString))
                {
                    var ss = valueForSet as SecureString;
                    var hasStr = ss.SecureStringToHash();
                    sqlParameter.Value = hasStr;
                }
                else
                {
                    sqlParameter.Value = valueForSet;
                }

                return sqlParameter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get Object from Sql Parameters
        /// </summary>
        /// <typeparam name="T">Type of the object to return</typeparam>
        /// <param name="sqlParameters">SqlParameters collection</param>
        /// <returns></returns>
        public static T GetObjectFromSqlParameters<T>(SqlParameter[] sqlParameters)
        {
            Logger.Log.Debug("SqlParameterTools. GetObjectFromSqlParameters");

            if (sqlParameters == null)
                throw new ArgumentNullException(nameof(sqlParameters));

            try
            {
                var tObj = Activator.CreateInstance<T>();
                Type type = tObj.GetType();
                PropertyInfo[] allProperties = type.GetProperties();
                PropertyInfo[] properties = allProperties.Where(x => Attribute.IsDefined(x, typeof(ParametersAttribute), false)).ToArray();
                foreach (var property in properties)
                {
                    var parametersAttr = property.GetCustomAttribute<ParametersAttribute>();
                    if (parametersAttr == null)
                        continue;

                    var parameter = sqlParameters.Where(a => a.ParameterName == parametersAttr.Name).FirstOrDefault();
                    if (parameter == null || parameter.Value == null || (object)parameter.Value == DBNull.Value ||
                        parameter.Direction == ParameterDirection.Input)
                        continue;

                    property.SetValue(tObj, parameter.Value);
                }

                return tObj;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
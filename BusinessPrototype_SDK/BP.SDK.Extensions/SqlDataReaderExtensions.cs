using System;
using System.Data.SqlClient;

using BP.SDK.Log;

namespace BP.SDK.Extensions
{
    /// <summary>
    /// Implements SqlDataReader Extensions functionality
    /// 
    /// 2018/11/06 - Created, VTyagunov
    /// </summary>
    public static class SqlDataReaderExtensions
    {
        /// <summary>
        /// Use for Get Data Type from Sql Data Reader
        /// </summary>
        /// <typeparam name="T">Type of the Data object</typeparam>
        /// <param name="reader">SqlDataReader object</param>
        /// <param name="name">Name of the column</param>
        /// <param name="defaulValue">Default value of the column</param>
        /// <returns></returns>
        public static T GetDataType<T>(this SqlDataReader reader, string name, object defaulValue = null)
        {
            Logger.Log.Debug("SqlDataReaderExtensions. GetDataType");

            try
            {
                var column = reader.GetOrdinal(name);
                return reader.IsDBNull(column) ? (T)defaulValue : (T)reader[name];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
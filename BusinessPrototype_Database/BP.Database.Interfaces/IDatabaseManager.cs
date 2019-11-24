using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using BP.Database.Base.Delegates;

namespace BP.Database.Interfaces
{
    /// <summary>
    /// Presents Database Manager functionality interface
    /// 
    /// 2018/02/10 - Created, VTyagunov
    /// </summary>
    public interface IDatabaseManager : IDisposable
    {
        /// <summary>Object of the Base functionality of the Database</summary>
        IDatabase DatabaseBase { get; }
        /// <summary>DB connection string</summary>
        string ConnectionString { get; }

        /// <summary>
        /// Use for Execute Non Query with some queries in a string with some GO commands
        /// </summary>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        void ExecuteNonQuery(string sqlQuery, CommandType cmdType, SqlParameter[] parameters);

        /// <summary>
        /// Use for Execute Scalar with some queries in a string with some GO commands
        /// </summary>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        object ExecuteScalar(string sqlQuery, CommandType cmdType, SqlParameter[] parameters);

        /// <summary>
        /// Use for Execute Query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        T Execute<T>(string sqlQuery, CommandType cmdType, SqlParameter[] parameters, Populator<T> populator);

        /// <summary>
        /// Use for Execute List Query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        List<T> ExecuteList<T>(string sqlQuery, CommandType cmdType, SqlParameter[] parameters,
            Populator<T> populator) where T : new();

        /// <summary>
        /// Use for Execute Non Query with some queries in a string with some GO commands
        /// </summary>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        void ExecuteNonQueryTransaction(string sqlQuery, CommandType cmdType, SqlParameter[] parameters);

        /// <summary>
        /// Use for Execute Scalar with some queries in a string with some GO commands
        /// </summary>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        object ExecuteScalarTransaction(string sqlQuery, CommandType cmdType, SqlParameter[] parameters);

        /// <summary>
        /// Use for Execute Query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        T ExecuteTransaction<T>(string sqlQuery, CommandType cmdType, SqlParameter[] parameters, Populator<T> populator);

        /// <summary>
        /// Use for Execute List Query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        List<T> ExecuteListTransaction<T>(string sqlQuery, CommandType cmdType, SqlParameter[] parameters,
            Populator<T> populator) where T : new();
    }
}
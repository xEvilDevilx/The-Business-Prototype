using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using BP.Database.Base.Delegates;
using BP.Database.Base.Enums;

namespace BP.Database.Interfaces
{
    /// <summary>
    /// Presents Database operations functionality interface
    /// 
    /// 2018/02/01- Created, VTyagunov
    /// </summary>
    public interface IDatabase
    {
        /// <summary>Database State Type object</summary>
        DBStateTypes DBState { get; }

        /// <summary>
        /// Use for Check Sql Server exists
        /// </summary>
        /// <returns></returns>
        bool CheckSqlServerExists();

        /// <summary>
        /// Use for Check Database exists
        /// </summary>
        /// <param name="dbName">The Database name</param>
        /// <returns></returns>
        bool CheckDBExists(string dbName);

        /// <summary>
        /// Use for Check Tabke Exists
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <returns></returns>
        bool CheckTableExists(string tableName);

        /// <summary>
        /// Use for Open connection
        /// </summary>
        void OpenConnection();

        /// <summary>
        /// Use for Open connection
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        void OpenConnection(string connectionString);

        /// <summary>
        /// Use for Close connection
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// Use for start Begin Transaction actions
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Use for Commit transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Use for RollBack transaction
        /// </summary>
        void Rollback();

        /// <summary>
        /// Use for Execute Non Query sql
        /// </summary>
        /// <param name="sql">Sql query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        int ExecuteNonQuery(string sql, CommandType cmdType, SqlParameter[] parameters);

        /// <summary>
        /// Use for Execute Scalar sql
        /// </summary>
        /// <param name="sql">Sql query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        object ExecuteScalar(string sql, CommandType cmdType, SqlParameter[] parameters);

        /// <summary>
        /// Use for Execute sql query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sql">Sql query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        T Execute<T>(string sql, CommandType cmdType, SqlParameter[] parameters, Populator<T> populator);

        /// <summary>
        /// Use for Execute sql query to List
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sql">Sql query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        List<T> ExecuteList<T>(string sql, CommandType cmdType, SqlParameter[] parameters,
            Populator<T> populator) where T : new();
    }
}
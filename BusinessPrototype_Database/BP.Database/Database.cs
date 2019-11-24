using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using BP.Database.Base.Delegates;
using BP.Database.Base.Enums;
using BP.Database.Interfaces;
using BP.SDK.Log;

namespace BP.Database
{
    /// <summary>
    /// Implements Database operations functionality
    /// 
    /// 2018/02/01 - Created, VTyagunov
    /// </summary>
    internal class Database : IDatabase
    {
        #region Variables

        /// <summary>The Connection string</summary>
        private string _connectionString;
        /// <summary>Sql Connection object</summary>
        private SqlConnection _connection = null;
        /// <summary>Sql Transaction object</summary>
        private SqlTransaction _transaction = null;

        #endregion

        #region Properties

        /// <summary>Database State Type object</summary>
        public DBStateTypes DBState { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">The Connection string</param>
        public Database(string connectionString)
        {
            Logger.Log.Debug("Database. Ctr. Enter");

            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);

            Logger.Log.Debug("Database. Ctr. Exit");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use for Check Sql Server exists
        /// </summary>
        /// <returns></returns>
        public bool CheckSqlServerExists()
        {
            Logger.Log.Debug("Database. CheckSqlServerExists. Enter");

            if (_connection == null)
            {
                DBState = DBStateTypes.SqlServerNotInstalled;
                Logger.Log.Warn("Database. CheckSqlServerExists. MS Sql Server does not exists!");
                return false;
            }

            Logger.Log.Debug("Database. CheckSqlServerExists. Exit");
            return true;
        }

        /// <summary>
        /// Use for Check Database exists
        /// </summary>
        /// <param name="dbName">The Database name</param>
        /// <returns></returns>
        public bool CheckDBExists(string dbName)
        {
            Logger.Log.Debug("Database. CheckDBExists. Enter");

            bool result = false;

            try
            {
                var tmpConnectionStr = DatabaseUtils.CreateTempConnectionString(_connection.DataSource);
                var tmpConn = new SqlConnection(tmpConnectionStr);
                var sqlQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", dbName);

                using (tmpConn)
                {
                    using (var sqlCmd = new SqlCommand(sqlQuery, tmpConn))
                    {
                        tmpConn.Open();

                        var resultObj = sqlCmd.ExecuteScalar();
                        int databaseID = 0;

                        if (resultObj != null)
                            int.TryParse(resultObj.ToString(), out databaseID);

                        tmpConn.Close();

                        result = (databaseID > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            if(!result)
                DBState = DBStateTypes.DatabaseNotExists;

            Logger.Log.Debug("Database. CheckDBExists. Exit");
            return result;
        }

        /// <summary>
        /// Use for Check Tabke Exists
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <returns></returns>
        public bool CheckTableExists(string tableName)
        {
            Logger.Log.Debug("Database. CheckTableExists. Enter");

            bool result = false;

            try
            {
                int tableCount = 0;
                var sqlQuery = string.Format(
                    "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{0}'", tableName);
                var sqlCmd = new SqlCommand(sqlQuery, _connection);
                _connection.Open();

                var resultObj = sqlCmd.ExecuteScalar();                
                if (resultObj != null)
                    int.TryParse(resultObj.ToString(), out tableCount);

                _connection.Close();

                result = (tableCount > 0);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Database. CheckTableExists. Error occured in a check table exists", ex);
                result = false;
            }

            Logger.Log.Debug("Database. CheckTableExists. Exit");
            return result;
        }

        /// <summary>
        /// Use for Open connection
        /// </summary>
        public void OpenConnection()
        {
            Logger.Log.Debug("Database. OpenConnection. Enter");

            _connection.Open();

            Logger.Log.Debug("Database. OpenConnection. Exit");
        }

        /// <summary>
        /// Use for Open connection
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public void OpenConnection(string connectionString)
        {
            Logger.Log.Debug("Database. OpenConnection. Enter");

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            Logger.Log.Debug("Database. OpenConnection. Exit");
        }

        /// <summary>
        /// Use for Close connection
        /// </summary>
        public void CloseConnection()
        {
            Logger.Log.Debug("Database. CloseConnection. Enter");

            _connection.Close();

            Logger.Log.Debug("Database. CloseConnection. Exit");
        }

        /// <summary>
        /// Use for start Begin Transaction actions
        /// </summary>
        public void BeginTransaction()
        {
            Logger.Log.Debug("Database. BeginTransaction. Enter");

            _transaction = _connection.BeginTransaction();

            Logger.Log.Debug("Database. BeginTransaction. Exit");
        }

        /// <summary>
        /// Use for Commit transaction
        /// </summary>
        public void Commit()
        {
            Logger.Log.Debug("Database. Commit. Enter");

            _transaction.Commit();
            _transaction = null;

            Logger.Log.Debug("Database. Commit. Exit");
        }

        /// <summary>
        /// Use for RollBack transaction
        /// </summary>
        public void Rollback()
        {
            Logger.Log.Debug("Database. Rollback. Enter");

            _transaction.Rollback();
            _transaction = null;

            Logger.Log.Debug("Database. Rollback. Exit");
        }

        /// <summary>
        /// Use for Execute Non Query sql
        /// </summary>
        /// <param name="sql">Sql query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        public int ExecuteNonQuery(string sql, CommandType cmdType, SqlParameter[] parameters)
        {
            Logger.Log.Debug("Database. ExecuteNonQuery. Enter");

            try
            {
                var command = new SqlCommand(sql, _connection)
                {
                    CommandType = cmdType,
                    Transaction = _transaction,
                    CommandTimeout = 0
                };

                if(parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);

                Logger.Log.Debug("Database. ExecuteNonQuery. Exit");
                return command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Logger.Log.Error("Database. ExecuteNonQuery. Execute sql query failed. ", ex);
                throw;
            }            
        }

        /// <summary>
        /// Use for Execute Scalar sql
        /// </summary>
        /// <param name="sql">Sql query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        public object ExecuteScalar(string sql, CommandType cmdType, SqlParameter[] parameters)
        {
            Logger.Log.Debug("Database. ExecuteScalar. Enter");

            try
            {
                var command = new SqlCommand(sql, _connection)
                {
                    CommandType = cmdType,
                    Transaction = _transaction,
                    CommandTimeout = 0
                };

                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);

                Logger.Log.Debug("Database. ExecuteScalar. Exit");
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Database. ExecuteScalar. Execute sql query failed. ", ex);
                throw;
            }
        }

        /// <summary>
        /// Use for Execute sql query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sql">Sql query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        public T Execute<T>(string sql, CommandType cmdType, SqlParameter[] parameters, Populator<T> populator)
        {
            Logger.Log.Debug("Database. Execute. Enter");

            T result;

            try
            {
                var command = new SqlCommand(sql, _connection);

                command.Parameters.AddRange(parameters);
                command.CommandType = cmdType;
                command.Transaction = _transaction;
                command.CommandTimeout = 0;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        result = populator(reader);
                    else throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Database. Execute. Execute sql query failed. ", ex);
                throw;
            }

            Logger.Log.Debug("Database. Execute. Exit");
            return result;
        }

        /// <summary>
        /// Use for Execute sql query to List
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sql">Sql query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        public List<T> ExecuteList<T>(string sql, CommandType cmdType, SqlParameter[] parameters,
            Populator<T> populator) where T : new()
        {
            Logger.Log.Debug("Database. ExecuteList. Enter");

            List<T> result = new List<T>();

            try
            {
                var command = new SqlCommand(sql, _connection);

                command.Parameters.AddRange(parameters);
                command.CommandType = cmdType;
                command.Transaction = _transaction;
                command.CommandTimeout = 0;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        result.Add(populator(reader));
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Database. ExecuteList. Execute list from sql query failed. ", ex);
                throw;
            }

            Logger.Log.Debug("Database. ExecuteList. Exit");
            return result;
        }

        #endregion
    }
}
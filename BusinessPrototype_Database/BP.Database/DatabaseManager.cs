using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using BP.Database.Base.Constants;
using BP.Database.Base.Delegates;
using BP.Database.Base.Enums;
using BP.Database.Interfaces;
using BP.Database.Properties;
using BP.SDK.Log;

namespace BP.Database
{
    /// <summary>
    /// Implements Database Manager functionality
    /// 
    /// 2018/09/09 - Created, VTyagunov
    /// </summary>
    public class DatabaseManager : IDatabaseManager
    {
        #region Variables

        /// <summary>Flag for IDisposable pattern</summary>
        private bool _disposed = false;
        /// <summary>DB connection string</summary>
        private string _connectionString;
        /// <summary>Database functionality</summary>
        private IDatabase _database;

        #endregion

        #region Properties

        /// <summary>Object of the Base functionality of the Database</summary>
        public IDatabase DatabaseBase
        {
            get { return _database; }
            private set
            {
                if(value != null)
                    _database = value;
            }
        }

        /// <summary>DB connection string</summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _connectionString = value;
            }
        }

        #endregion

        #region Events

        /// <summary>Database Event Handler</summary>
        public event DatabaseEventHandler DatabaseEvent;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseManager()
        {
            Logger.Log.Debug("DatabaseManager. Ctr. Enter");

            try
            {
                _connectionString = ReadConnectionStringFromConfig();
                _database = new Database(_connectionString);
                //ValidateDatabase();
                var sqlServerExists = _database.CheckSqlServerExists();
                if (!sqlServerExists)
                {
                    SendEvent(_database.DBState, null);
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. Ctr. ", ex);
                throw;
            }

            Logger.Log.Debug("DatabaseManager. Ctr. Exit");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">The Connection string</param>
        public DatabaseManager(string connectionString)
        {
            Logger.Log.Debug("DatabaseManager. Ctr. Enter");

            try
            {
                _connectionString = connectionString;
                _database = new Database(_connectionString);
                //ValidateDatabase();
                var sqlServerExists = _database.CheckSqlServerExists();
                if (!sqlServerExists)
                {
                    SendEvent(_database.DBState, null);
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. Ctr. ", ex);
                throw;
            }

            Logger.Log.Debug("DatabaseManager. Ctr. Exit");
        }

        #endregion

        #region Destructor

        /// <summary>
        /// Destructor
        /// </summary>
        ~DatabaseManager()
        {
            Logger.Log.Debug("DatabaseManager. Dtr. Enter");

            Dispose(false);

            Logger.Log.Debug("DatabaseManager. Dtr. Exit");
        }

        #endregion

        #region Methods

        #region IDisposable implementation

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        public void Dispose()
        {
            Logger.Log.Debug("DatabaseManager. Dispose. Enter");

            Dispose(true);
            GC.SuppressFinalize(this);

            Logger.Log.Debug("DatabaseManager. Dispose. Exit");
        }

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        /// <param name="disposing">Flag shows is need dispose or not need</param>
        protected virtual void Dispose(bool disposing)
        {
            Logger.Log.Debug("DatabaseManager. Dispose. Enter");

            if (!_disposed)
            {
                if (disposing)
                    _database = null;

                _disposed = true;
            }

            Logger.Log.Debug("DatabaseManager. Dispose. Exit");
        }

        #endregion

        #region Execute

        /// <summary>
        /// Use for Execute Non Query with some queries in a string with some GO commands
        /// </summary>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        public void ExecuteNonQuery(string sqlQuery, CommandType cmdType, SqlParameter[] parameters)
        {
            Logger.Log.Debug("DatabaseManager. ExecuteNonQuery. Enter");

            try
            {
                _database.OpenConnection(_connectionString);

                switch (cmdType)
                {
                    case CommandType.StoredProcedure:
                    case CommandType.TableDirect:
                        _database.ExecuteNonQuery(sqlQuery, cmdType, parameters);
                        break;
                    case CommandType.Text:
                        foreach (string part in GetSqlParts(sqlQuery))
                            _database.ExecuteNonQuery(part, cmdType, parameters);
                        break;
                }

                _database.CloseConnection();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. ExecuteNonQuery.", ex);
                SendEvent(DBStateTypes.ExecuteFailed, ex);
                _database.CloseConnection();
                throw;
            }

            Logger.Log.Debug("DatabaseManager. ExecuteNonQuery. Exit");
        }

        /// <summary>
        /// Use for Execute Scalar with some queries in a string with some GO commands
        /// </summary>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        public object ExecuteScalar(string sqlQuery, CommandType cmdType, SqlParameter[] parameters)
        {
            Logger.Log.Debug("DatabaseManager. ExecuteScalar. Enter");

            try
            {
                _database.OpenConnection(_connectionString);

                object resultObj = null;

                switch (cmdType)
                {
                    case CommandType.StoredProcedure:
                    case CommandType.TableDirect:
                        resultObj = _database.ExecuteScalar(sqlQuery, cmdType, parameters);
                        break;
                    case CommandType.Text:
                        foreach (string part in GetSqlParts(sqlQuery))
                            resultObj = _database.ExecuteScalar(part, cmdType, parameters);
                        break;
                }

                _database.CloseConnection();
                
                Logger.Log.Debug("DatabaseManager. ExecuteScalar. Exit");
                return resultObj;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. ExecuteScalar.", ex);
                SendEvent(DBStateTypes.ExecuteFailed, ex);
                _database.CloseConnection();
                throw;
            }
        }

        /// <summary>
        /// Use for Execute Query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        public T Execute<T>(string sqlQuery, CommandType cmdType, SqlParameter[] parameters, Populator<T> populator)
        {
            Logger.Log.Debug("DatabaseManager. Execute. Enter");

            try
            {
                _database.OpenConnection(_connectionString);
                var obj = _database.Execute<T>(sqlQuery, cmdType, parameters, populator);
                _database.CloseConnection();

                Logger.Log.Debug("DatabaseManager. Execute. Enter");
                return obj;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. Execute.", ex);
                SendEvent(DBStateTypes.ExecuteFailed, ex);
                _database.CloseConnection();
                throw;
            }
        }

        /// <summary>
        /// Use for Execute List Query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        public List<T> ExecuteList<T>(string sqlQuery, CommandType cmdType, SqlParameter[] parameters,
            Populator<T> populator) where T : new()
        {
            Logger.Log.Debug("DatabaseManager. ExecuteList. Enter");

            try
            {
                _database.OpenConnection(_connectionString);
                var obj = _database.ExecuteList<T>(sqlQuery, cmdType, parameters, populator);
                _database.CloseConnection();

                Logger.Log.Debug("DatabaseManager. ExecuteList. Enter");
                return obj;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. ExecuteList.", ex);
                SendEvent(DBStateTypes.ExecuteFailed, ex);
                _database.CloseConnection();
                throw;
            }
        }

        #endregion

        #region Execute with Transaction

        /// <summary>
        /// Use for Execute Non Query with some queries in a string with some GO commands
        /// </summary>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        public void ExecuteNonQueryTransaction(string sqlQuery, CommandType cmdType, SqlParameter[] parameters)
        {
            Logger.Log.Debug("DatabaseManager. ExecuteNonQueryAdvanced. Enter");

            try
            {
                _database.OpenConnection(_connectionString);
                _database.BeginTransaction();

                switch (cmdType)
                {
                    case CommandType.StoredProcedure:
                    case CommandType.TableDirect:
                        _database.ExecuteNonQuery(sqlQuery, cmdType, parameters);
                        break;
                    case CommandType.Text:
                        foreach (string part in GetSqlParts(sqlQuery))
                            _database.ExecuteNonQuery(part, cmdType, parameters);
                        break;
                }

                _database.Commit();
                _database.CloseConnection();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. ExecuteNonQueryAdvanced.", ex);
                SendEvent(DBStateTypes.ExecuteFailed, ex);
                _database.Rollback();
                _database.CloseConnection();
                throw;
            }

            Logger.Log.Debug("DatabaseManager. ExecuteNonQueryAdvanced. Exit");
        }

        /// <summary>
        /// Use for Execute Scalar with some queries in a string with some GO commands
        /// </summary>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql command type</param>
        /// <param name="parameters">Sql Parameters</param>
        public object ExecuteScalarTransaction(string sqlQuery, CommandType cmdType, SqlParameter[] parameters)
        {
            Logger.Log.Debug("DatabaseManager. ExecuteScalarTransaction. Enter");

            try
            {
                object resultObj = null;

                _database.OpenConnection(_connectionString);
                _database.BeginTransaction();

                switch (cmdType)
                {
                    case CommandType.StoredProcedure:
                    case CommandType.TableDirect:
                        resultObj = _database.ExecuteScalar(sqlQuery, cmdType, parameters);
                        break;
                    case CommandType.Text:
                        foreach (string part in GetSqlParts(sqlQuery))
                            resultObj = _database.ExecuteScalar(part, cmdType, parameters);
                        break;
                }

                _database.Commit();
                _database.CloseConnection();

                Logger.Log.Debug("DatabaseManager. ExecuteScalarTransaction. Exit");
                return resultObj;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. ExecuteScalarTransaction.", ex);
                SendEvent(DBStateTypes.ExecuteFailed, ex);
                _database.Rollback();
                _database.CloseConnection();
                throw;
            }
        }

        /// <summary>
        /// Use for Execute Query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        public T ExecuteTransaction<T>(string sqlQuery, CommandType cmdType, SqlParameter[] parameters, Populator<T> populator)
        {
            Logger.Log.Debug("DatabaseManager. Execute. Enter");

            try
            {
                _database.OpenConnection(_connectionString);
                _database.BeginTransaction();
                var obj = _database.Execute<T>(sqlQuery, cmdType, parameters, populator);
                _database.Commit();
                _database.CloseConnection();

                Logger.Log.Debug("DatabaseManager. Execute. Enter");
                return obj;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. Execute.", ex);
                SendEvent(DBStateTypes.ExecuteFailed, ex);
                _database.Rollback();
                _database.CloseConnection();
                throw;
            }
        }

        /// <summary>
        /// Use for Execute List Query
        /// </summary>
        /// <typeparam name="T">Type of returning object</typeparam>
        /// <param name="sqlQuery">Sql Query</param>
        /// <param name="cmdType">Sql Command Type</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="populator">Populator method link</param>
        /// <returns>T type object</returns>
        public List<T> ExecuteListTransaction<T>(string sqlQuery, CommandType cmdType, SqlParameter[] parameters,
            Populator<T> populator) where T : new()
        {
            Logger.Log.Debug("DatabaseManager. ExecuteList. Enter");

            try
            {
                _database.OpenConnection(_connectionString);
                _database.BeginTransaction();
                var obj = _database.ExecuteList<T>(sqlQuery, cmdType, parameters, populator);
                _database.Commit();
                _database.CloseConnection();

                Logger.Log.Debug("DatabaseManager. ExecuteList. Enter");
                return obj;
            }
            catch (Exception ex)
            {
                Logger.Log.Error("DatabaseManager. ExecuteList.", ex);
                SendEvent(DBStateTypes.ExecuteFailed, ex);
                _database.Rollback();
                _database.CloseConnection();
                throw;
            }
        }

        #endregion

        /// <summary>
        /// Use for split sql query and get sql parts
        /// </summary>
        /// <param name="sql">Sql query</param>
        /// <returns></returns>
        private IEnumerable<string> GetSqlParts(string sql)
        {
            const string reBatchSeparator = @"^(/\*.\*/)?\s*GO\s*(/\*.*\*/)?\s*(--.*)?$";

            using (var sr = new StringReader(sql))
            {
                string line;
                string part;
                var sb = new StringBuilder();
                var regex = new Regex(reBatchSeparator);
                while ((line = sr.ReadLine()) != null)
                {
                    if (!regex.IsMatch(line))
                        sb.AppendLine(line);
                    else
                    {
                        part = sb.ToString();
                        if (!string.IsNullOrEmpty(part))
                            yield return part;
                        sb.Clear();
                    }
                }

                part = sb.ToString();
                if (!string.IsNullOrEmpty(part))
                    yield return part;
            }
        }

        /// <summary>
        /// Use for Send Event to all subscribers
        /// </summary>
        /// <param name="dbState">DB State</param>
        /// <param name="detailsObject">Object for transfer details into event</param>
        private void SendEvent(DBStateTypes dbState, object detailsObject)
        {
            if (DatabaseEvent != null)
                DatabaseEvent(dbState, detailsObject);
        }

        /// <summary>
        /// Use for Validate the database
        /// </summary>
        private void ValidateDatabase()
        {
            var sqlServerExists = _database.CheckSqlServerExists();
            if (!sqlServerExists)
            {
                SendEvent(_database.DBState, null);
                Dispose();
            }

            //var dbExists = _database.CheckDBExists();
            //if (!dbExists)
            //{
            //    SendEvent(_database.DBState, null);
            //    Dispose();
            //}
        }

        /// <summary>
        /// Use for Read Connection String From Config file
        /// </summary>
        /// <returns></returns>
        private string ReadConnectionStringFromConfig()
        {
            Logger.Log.Debug("DatabaseManager. ReadConnectionStringFromConfig. Enter");

            var config = (NameValueCollection)ConfigurationManager.GetSection(DatabaseConfigConstants.DBConfigSection);
            if (config == null)
            {
                var message = string.Format(Resource.NoDBSection, DatabaseConfigConstants.DBConfigSection);
                Logger.Log.WarnFormat("DatabaseManager. ReadConnectionStringFromConfig. {0}", message);
                SendEvent(DBStateTypes.ReadConnectionStringFailed, message);
                return string.Empty;
            }

            var sqlServerNameStr = config[DatabaseConfigConstants.DBSqlServerNameConfigTag];
            var dbNameStr = config[DatabaseConfigConstants.DBNameConfigTag];
            var windowsAuthStr = config[DatabaseConfigConstants.DBWindowsAuthConfigTag];
            var dbUserNameStr = config[DatabaseConfigConstants.DBUserNameConfigTag];
            var dbUserPasswordStr = config[DatabaseConfigConstants.DBUserPasswordConfigTag];
            var connectionTypeStr = config[DatabaseConfigConstants.DBConnectionTypeConfigTag];

            var isWindowsAuth = false;
            if (windowsAuthStr == "1")
                isWindowsAuth = true;

            var connectionType = (ConnectionTypes)byte.Parse(connectionTypeStr);
            var connectionString = DatabaseUtils.CreateConnectionString(sqlServerNameStr, dbNameStr, isWindowsAuth,
                    dbUserNameStr, dbUserPasswordStr, connectionType);

            Logger.Log.Debug("DatabaseManager. ReadConnectionStringFromConfig. Exit");
            return connectionString;
        }

        #endregion
    }
}
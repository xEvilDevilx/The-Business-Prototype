using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using BP.Database;
using BP.Database.Interfaces;
using BP.DataLayer.Base;
using BP.DataLayer.Base.Enums;
using BP.DataLayer.Databases.Base;
using BP.DataLayer.Databases.Base.Enums;
using BP.DataLayer.Interfaces;
using BP.DataLayer.Tools;
using BP.SDK.Log;

namespace BP.DataLayer.DataProviders
{
    /// <summary>
    /// Implements functionality for common database queries
    /// 
    /// 2018/11/18 - Created, VTyagunov
    /// </summary>
    public class DataProviderManager : IDataProviderManager
    {
        #region Variables

        /// <summary>Flag for IDisposable pattern</summary>
        private bool _disposed = false;
        /// <summary>Connection String</summary>
        private string _connectionString;
        /// <summary>Database Manager</summary>
        private IDatabaseManager _databaseManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        public DataProviderManager(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("DataProviderManager. Ctr");

            _connectionString = connectionSettings.ConnectionString;
            _databaseManager = new DatabaseManager(_connectionString);
        }

        #endregion

        #region Methods

        #region IDisposable implementation

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        public void Dispose()
        {
            Logger.Log.Debug("DataProviderManager. Dispose");

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        /// <param name="disposing">Flag shows is need dispose or not need</param>
        protected virtual void Dispose(bool disposing)
        {
            Logger.Log.Debug("DataProviderManager. Dispose");

            if (!_disposed)
            {
                if (disposing)
                    _databaseManager.Dispose();

                _disposed = true;
            }
        }

        #endregion

        public DatabaseQueryResult ExecuteNoneQuerySP<TObject, TPropertiesEnum>(string storedProcName,
            TObject tObj, TPropertiesEnum tPropertiesEnumType)
        {
            Logger.Log.Debug("DataProviderManager. ExecuteNoneQuerySP");

            if (string.IsNullOrEmpty(storedProcName))
                throw new ArgumentNullException(storedProcName);

            if (tObj == null)
                throw new ArgumentNullException(nameof(tObj));

            if (!typeof(TPropertiesEnum).IsEnum)
                throw new ArgumentException("TPropertiesEnum must be an enumerated type");

            try
            {
                var sqlParameters = SqlParameterTools.CreateSqlParameters(tObj, tPropertiesEnumType);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Update);
                var parameters = new SqlParameter[sqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(sqlParameters, parameters, sqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, sqlParameters.Length, resultSqlParameters.Length);
                
                _databaseManager.ExecuteNonQueryTransaction(storedProcName, CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("DataProviderManager. ExecuteNoneQuerySP. Execute Non Query operation success. Stored Proc name: '{0}'", storedProcName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DatabaseQueryResult<object> ExecuteScalarSP<TObject, TPropertiesEnum>(string storedProcName,
            TObject tObj, TPropertiesEnum tPropertiesEnumType)
        {
            Logger.Log.Debug("DataProviderManager. ExecuteSP");

            if (string.IsNullOrEmpty(storedProcName))
                throw new ArgumentNullException(storedProcName);

            if (tObj == null)
                throw new ArgumentNullException(nameof(tObj));

            if (!typeof(TPropertiesEnum).IsEnum)
                throw new ArgumentException("TPropertiesEnum must be an enumerated type");

            try
            {
                var sqlParameters = SqlParameterTools.CreateSqlParameters(tObj, tPropertiesEnumType);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Update);
                var parameters = new SqlParameter[sqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(sqlParameters, parameters, sqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, sqlParameters.Length, resultSqlParameters.Length);

                object resultObj = _databaseManager.ExecuteScalarTransaction(storedProcName, CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<object>(resultParamOut, errorMessageOut, resultObj);

                Logger.Log.InfoFormat("DataProviderManager. ExecuteScalarSP. Execute operation success. Stored Proc name: '{0}'", storedProcName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DatabaseQueryResult<TReturn> ExecuteSP<TReturn, TObject, TPropertiesEnum>(string storedProcName,
            TObject tObj, TPropertiesEnum tPropertiesEnumType)
        {
            Logger.Log.Debug("DataProviderManager. ExecuteSP");

            if (string.IsNullOrEmpty(storedProcName))
                throw new ArgumentNullException(storedProcName);

            if (tObj == null)
                throw new ArgumentNullException(nameof(tObj));

            if (!typeof(TPropertiesEnum).IsEnum)
                throw new ArgumentException("TPropertiesEnum must be an enumerated type");

            try
            {
                var sqlParameters = SqlParameterTools.CreateSqlParameters(tObj, tPropertiesEnumType);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Update);
                var parameters = new SqlParameter[sqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(sqlParameters, parameters, sqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, sqlParameters.Length, resultSqlParameters.Length);

                TReturn resultObj = default(TReturn);// _databaseManager.ExecuteTransaction<TReturn>(storedProcName, CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<TReturn>(resultParamOut, errorMessageOut, resultObj);

                Logger.Log.InfoFormat("DataProviderManager. ExecuteSP. Execute operation success. Stored Proc name: '{0}'", storedProcName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DatabaseQueryResult<List<TReturn>> ExecuteListSP<TReturn, TObject, TPropertiesEnum>(string storedProcName,
            TObject tObj, TPropertiesEnum tPropertiesEnumType)
        {
            return null;
        }

        #endregion
    }
}
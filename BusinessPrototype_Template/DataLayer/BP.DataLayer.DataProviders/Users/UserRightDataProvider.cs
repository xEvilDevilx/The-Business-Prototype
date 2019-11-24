using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using BP.Database;
using BP.Database.Interfaces;
using BP.DataLayer.Base;
using BP.DataLayer.Base.Enums;
using BP.DataLayer.BusinessObjects.Users;
using BP.DataLayer.Databases.Base;
using BP.DataLayer.Databases.Base.Enums;
using BP.DataLayer.Interfaces;
using BP.DataLayer.Tools;
using BP.SDK.Extensions;
using BP.SDK.Log;

namespace BP.DataLayer.DataProviders.Users
{
    /// <summary>
    /// Implements the User Right Data Provider functionality
    /// 
    /// 2018/11/13 - Created, IMasenko
    /// </summary>
    public class UserRightDataProvider : IDataProvider<Right, RightParameters>
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
        public UserRightDataProvider(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("UserRightDataProvider. Ctr");

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
            Logger.Log.Debug("UserRightDataProvider. Dispose");

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        /// <param name="disposing">Flag shows is need dispose or not need</param>
        protected virtual void Dispose(bool disposing)
        {
            Logger.Log.Debug("UserRightDataProvider. Dispose");

            if (!_disposed)
            {
                if (disposing)
                    _databaseManager.Dispose();

                _disposed = true;
            }
        }

        #endregion

        /// <summary>
        /// Use for Add User Right to the table
        /// </summary>
        /// <param name="userRightParameters">User Right parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Add(RightParameters userRightParameters)
        {
            Logger.Log.Debug("UserRightDataProvider. Add");

            if (userRightParameters == null)
                throw new ArgumentNullException(nameof(userRightParameters));

            try
            {
                var userRightSqlParameters = SqlParameterTools.CreateSqlParameters<RightParameters>(userRightParameters, OperationKeyTypes.Add);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Add);
                var parameters = new SqlParameter[userRightSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userRightSqlParameters, parameters, userRightSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userRightSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_ADDNEWUSERRIGHT", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserRightDataProvider. Add. UserRight[RightName: '{0}'] successfully added to the database",
                    userRightParameters.RightName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Update the User Right in the table
        /// </summary>
        /// <param name="userRightParameters">User Right parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Update(RightParameters userRightParameters)
        {
            Logger.Log.Debug("UserRightDataProvider. Update");

            if (userRightParameters == null)
                throw new ArgumentNullException(nameof(userRightParameters));

            if ((userRightParameters.RightID == null) || (userRightParameters.RightID == Guid.Empty))
                throw new ArgumentException("Update. User Right ID cannot be null or empty!");

            try
            {
                var userRightSqlParameters = SqlParameterTools.CreateSqlParameters<RightParameters>(userRightParameters, OperationKeyTypes.Update);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Update);
                var parameters = new SqlParameter[userRightSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userRightSqlParameters, parameters, userRightSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userRightSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_UPDATEUSERRIGHT", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserRightDataProvider. Update. User Right[RightName: '{0}'] successfully updated in the database",
                    userRightParameters.RightName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get the User Right from table
        /// </summary>
        /// <param name="userRightParameters">User Right parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<Right> Get(RightParameters userRightParameters)
        {
            Logger.Log.Debug("UserRightDataProvider. Get");

            if (userRightParameters == null)
                throw new ArgumentNullException(nameof(userRightParameters));

            if ((userRightParameters.RightID == null) || (userRightParameters.RightID == Guid.Empty))
                throw new ArgumentException("Get. User Right ID cannot be null or empty!");

            try
            {
                var userRightSqlParameters = SqlParameterTools.CreateSqlParameters<RightParameters>(userRightParameters, OperationKeyTypes.Get);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Get);
                var parameters = new SqlParameter[userRightSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userRightSqlParameters, parameters, userRightSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userRightSqlParameters.Length, resultSqlParameters.Length);

                var newUserRight = _databaseManager.ExecuteTransaction<Right>("PROC_GETUSERRIGHT", CommandType.StoredProcedure, parameters, UserRightPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<Right>(resultParamOut, errorMessageOut, newUserRight);

                Logger.Log.InfoFormat("UserRightDataProvider. Get. User Right[RightName: '{0}'] get operation is successful",
                    userRightParameters.RightName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Populate a User Right object
        /// </summary>
        /// <param name="reader">SqlDataReader object</param>
        /// <returns></returns>
        private Right UserRightPopulator(SqlDataReader reader)
        {
            Logger.Log.Debug("UserRightDataProvider. UserRightPopulator");

            try
            {
                var userRight = new Right();
                userRight.RightID = reader.GetDataType<Guid>("USERRIGHTID");
                userRight.RightName = reader.GetDataType<string>("RIGHTNAME");
                userRight.RightNameTranslation = reader.GetDataType<string>("USERRIGHTLOCALIZEID");
                return userRight;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get List of User Rights from the table
        /// </summary>
        /// <param name="userRightParameters">User Right parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<List<Right>> GetList(RightParameters userRightParameters)
        {
            Logger.Log.Debug("UserRightDataProvider. GetList");

            try
            {
                var userRightSqlParameters = SqlParameterTools.CreateSqlParameters<RightParameters>(userRightParameters, OperationKeyTypes.GetList);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.GetList);
                var parameters = new SqlParameter[userRightSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userRightSqlParameters, parameters, userRightSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userRightSqlParameters.Length, resultSqlParameters.Length);

                var userRightList = _databaseManager.ExecuteListTransaction<Right>("PROC_GETUSERRIGHTLIST", CommandType.StoredProcedure, parameters, UserRightPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<List<Right>>(resultParamOut, errorMessageOut, userRightList);

                Logger.Log.InfoFormat("UserRightDataProvider. GetList. Get List with User Rights operation is successful");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Delete a User Right from the table
        /// </summary>
        /// <param name="userRightParameters">User Right parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Delete(RightParameters userRightParameters)
        {
            Logger.Log.Debug("UserRightDataProvider. Delete");

            if (userRightParameters == null)
                throw new ArgumentNullException(nameof(userRightParameters));

            if ((userRightParameters.RightID == null) || (userRightParameters.RightID == Guid.Empty))
                throw new ArgumentException("Delete. User Right ID cannot be null or empty!");

            try
            {
                var userRightSqlParameters = SqlParameterTools.CreateSqlParameters<RightParameters>(userRightParameters, OperationKeyTypes.Delete);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Delete);
                var parameters = new SqlParameter[userRightSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userRightSqlParameters, parameters, userRightSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userRightSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_DELETEUSERRIGHT", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserRightDataProvider. Delete. The User Right[Login: '{0}'] Delete operation is successful",
                    userRightParameters.RightName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Search the List of User Rights by Parameters
        /// </summary>
        /// <param name="userRightParameters">User Right parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<List<Right>> Search(RightParameters userRightParameters)
        {
            Logger.Log.Debug("UserRightDataProvider. Search");

            if (userRightParameters == null)
                throw new ArgumentNullException(nameof(userRightParameters));

            try
            {
                var userRightSqlParameters = SqlParameterTools.CreateSqlParameters<RightParameters>(userRightParameters, OperationKeyTypes.Search);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Search);
                var parameters = new SqlParameter[userRightSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userRightSqlParameters, parameters, userRightSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userRightSqlParameters.Length, resultSqlParameters.Length);

                var userRightList = _databaseManager.ExecuteListTransaction<Right>("PROC_SEARCHUSERRIGHTS", CommandType.StoredProcedure, parameters, UserRightPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<List<Right>>(resultParamOut, errorMessageOut, userRightList);

                Logger.Log.InfoFormat("UserRightDataProvider. Search. The Search operation is successful",
                    userRightParameters.RightName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
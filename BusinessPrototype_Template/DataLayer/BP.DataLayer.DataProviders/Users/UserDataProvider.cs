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
    /// Implements the User Data Provider functionality
    /// 
    /// 2018/10/23 - Created, VTyagunov
    /// </summary>
    public class UserDataProvider : IDataProvider<User, UserParameters>
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
        public UserDataProvider(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("UserDataProvider. Ctr");

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
            Logger.Log.Debug("UserDataProvider. Dispose");

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        /// <param name="disposing">Flag shows is need dispose or not need</param>
        protected virtual void Dispose(bool disposing)
        {
            Logger.Log.Debug("UserDataProvider. Dispose");

            if (!_disposed)
            {
                if (disposing)
                    _databaseManager.Dispose();

                _disposed = true;
            }
        }

        #endregion

        /// <summary>
        /// Use for Add User to the table
        /// </summary>
        /// <param name="userParameters">User parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Add(UserParameters userParameters)
        {
            Logger.Log.Debug("UserDataProvider. Add");

            if (userParameters == null)
                throw new ArgumentNullException(nameof(userParameters));

            try
            {
                var userSqlParameters = SqlParameterTools.CreateSqlParameters<UserParameters>(userParameters, OperationKeyTypes.Add);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Add);
                var parameters = new SqlParameter[userSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userSqlParameters, parameters, userSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_ADDNEWUSER", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserDataProvider. Add. User[Login: '{0}'] successfully added to the database",
                    userParameters.Login);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Update the User in the table
        /// </summary>
        /// <param name="userParameters">User parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Update(UserParameters userParameters)
        {
            Logger.Log.Debug("UserDataProvider. Update");

            if (userParameters == null)
                throw new ArgumentNullException(nameof(userParameters));

            if ((userParameters.ID == null) || (userParameters.ID == Guid.Empty))
                throw new ArgumentException("Update. User ID cannot be null or empty!");

            try
            {
                var userSqlParameters = SqlParameterTools.CreateSqlParameters<UserParameters>(userParameters, OperationKeyTypes.Update);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Update);
                var parameters = new SqlParameter[userSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userSqlParameters, parameters, userSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_UPDATEUSER", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserDataProvider. Update. User[Login: '{0}'] successfully updated in the database",
                    userParameters.Login);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get the User from table
        /// </summary>
        /// <param name="userParameters">User parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<User> Get(UserParameters userParameters)
        {
            Logger.Log.Debug("UserDataProvider. Get");

            if (userParameters == null)
                throw new ArgumentNullException(nameof(userParameters));

            if ((userParameters.ID == null) || (userParameters.ID == Guid.Empty))
                throw new ArgumentException("Get. User ID cannot be null or empty!");

            try
            {
                var userSqlParameters = SqlParameterTools.CreateSqlParameters<UserParameters>(userParameters, OperationKeyTypes.Get);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Get);
                var parameters = new SqlParameter[userSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userSqlParameters, parameters, userSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userSqlParameters.Length, resultSqlParameters.Length);

                var newUser =_databaseManager.ExecuteTransaction<User>("PROC_GETUSER", CommandType.StoredProcedure, parameters, UserPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<User>(resultParamOut, errorMessageOut, newUser);

                Logger.Log.InfoFormat("UserDataProvider. Get. User[Login: '{0}'] get operation is successful",
                    userParameters.Login);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Populate a User object
        /// </summary>
        /// <param name="reader">SqlDataReader object</param>
        /// <returns></returns>
        private User UserPopulator(SqlDataReader reader)
        {
            Logger.Log.Debug("UserDataProvider. UserPopulator");

            try
            {
                var user = new User();
                user.ID = reader.GetDataType<Guid>("USERID");
                user.UserGroupID = reader.GetDataType<Guid>("USERGROUPID");
                user.Login = reader.GetDataType<string>("LOGIN");
                user.PasswordHash = reader.GetDataType<string>("PASSWORD");
                user.FirstName = reader.GetDataType<string>("FIRSTNAME", string.Empty);
                user.MiddleName = reader.GetDataType<string>("MIDDLENAME", string.Empty);
                user.LastName = reader.GetDataType<string>("LASTNAME", string.Empty);
                user.CreateDateTime = reader.GetDataType<DateTime>("CREATIONDATETIME");
                user.LastLoginDateTime = reader.GetDataType<DateTime?>("LASTLOGINDATETIME");
                user.Blocked = reader.GetDataType<bool>("BLOCKED");
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get List of Users from the table
        /// </summary>
        /// <param name="userParameters">User parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<List<User>> GetList(UserParameters userParameters)
        {
            Logger.Log.Debug("UserDataProvider. GetList");

            try
            {
                var userSqlParameters = SqlParameterTools.CreateSqlParameters<UserParameters>(userParameters, OperationKeyTypes.GetList);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.GetList);
                var parameters = new SqlParameter[userSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userSqlParameters, parameters, userSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userSqlParameters.Length, resultSqlParameters.Length);

                var userList = _databaseManager.ExecuteListTransaction<User>("PROC_GETUSERLIST", CommandType.StoredProcedure, parameters, UserPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<List<User>>(resultParamOut, errorMessageOut, userList);

                Logger.Log.InfoFormat("UserDataProvider. GetList. Get List with Users operation is successful");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Delete the User from table
        /// </summary>
        /// <param name="userParameters">User parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Delete(UserParameters userParameters)
        {
            Logger.Log.Debug("UserDataProvider. Delete");

            if (userParameters == null)
                throw new ArgumentNullException(nameof(userParameters));

            if ((userParameters.ID == null) || (userParameters.ID == Guid.Empty))
                throw new ArgumentException("Delete. User ID cannot be null or empty!");

            try
            {
                var userSqlParameters = SqlParameterTools.CreateSqlParameters<UserParameters>(userParameters, OperationKeyTypes.Delete);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Delete);
                var parameters = new SqlParameter[userSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userSqlParameters, parameters, userSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_DELETEUSER", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserDataProvider. Delete. User[Login: '{0}'] Delete operation is successful",
                    userParameters.Login);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Search the List of Users by Parameters
        /// </summary>
        /// <param name="userParameters">User parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<List<User>> Search(UserParameters userParameters)
        {
            Logger.Log.Debug("UserDataProvider. Search");

            if (userParameters == null)
                throw new ArgumentNullException(nameof(userParameters));

            try
            {
                var userSqlParameters = SqlParameterTools.CreateSqlParameters<UserParameters>(userParameters, OperationKeyTypes.Search);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Search);
                var parameters = new SqlParameter[userSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userSqlParameters, parameters, userSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userSqlParameters.Length, resultSqlParameters.Length);

                var userList = _databaseManager.ExecuteListTransaction<User>("PROC_SEARCHUSERS", CommandType.StoredProcedure, parameters, UserPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<List<User>>(resultParamOut, errorMessageOut, userList);

                Logger.Log.InfoFormat("UserDataProvider. Search. The Search operation is successful",
                    userParameters.Login);
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
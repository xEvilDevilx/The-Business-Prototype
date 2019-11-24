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
    /// Implements the User Group Data Provider functionality
    /// 
    /// 2018/11/09 - Created, VTyagunov
    /// </summary>
    public class UserGroupDataProvider : IDataProvider<UserGroup, UserGroupParameters>
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
        public UserGroupDataProvider(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("UserGroupDataProvider. Ctr");

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
            Logger.Log.Debug("UserGroupDataProvider. Dispose");

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        /// <param name="disposing">Flag shows is need dispose or not need</param>
        protected virtual void Dispose(bool disposing)
        {
            Logger.Log.Debug("UserGroupDataProvider. Dispose");

            if (!_disposed)
            {
                if (disposing)
                    _databaseManager.Dispose();

                _disposed = true;
            }
        }

        #endregion

        /// <summary>
        /// Use for Add User Group to the table
        /// </summary>
        /// <param name="userGroupParameters">User Group parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Add(UserGroupParameters userGroupParameters)
        {
            Logger.Log.Debug("UserGroupDataProvider. Add");

            if (userGroupParameters == null)
                throw new ArgumentNullException(nameof(userGroupParameters));

            try
            {
                var userGroupSqlParameters = SqlParameterTools.CreateSqlParameters<UserGroupParameters>(userGroupParameters, OperationKeyTypes.Add);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Add);
                var parameters = new SqlParameter[userGroupSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userGroupSqlParameters, parameters, userGroupSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userGroupSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_ADDNEWUSERGROUP", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserGroupDataProvider. Add. UserGroup[GroupName: '{0}'] successfully added to the database",
                    userGroupParameters.GroupName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Update the User Group in the table
        /// </summary>
        /// <param name="userGroupParameters">User Group parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Update(UserGroupParameters userGroupParameters)
        {
            Logger.Log.Debug("UserGroupDataProvider. Update");

            if (userGroupParameters == null)
                throw new ArgumentNullException(nameof(userGroupParameters));

            if ((userGroupParameters.UserGroupID == null) || (userGroupParameters.UserGroupID == Guid.Empty))
                throw new ArgumentException("Update. User Group ID cannot be null or empty!");

            try
            {
                var userGroupSqlParameters = SqlParameterTools.CreateSqlParameters<UserGroupParameters>(userGroupParameters, OperationKeyTypes.Update);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Update);
                var parameters = new SqlParameter[userGroupSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userGroupSqlParameters, parameters, userGroupSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userGroupSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_UPDATEUSERGROUP", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserGroupDataProvider. Update. User Group[GroupName: '{0}'] successfully updated in the database",
                    userGroupParameters.GroupName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get the User Group from table
        /// </summary>
        /// <param name="userGroupParameters">User Group parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<UserGroup> Get(UserGroupParameters userGroupParameters)
        {
            Logger.Log.Debug("UserGroupDataProvider. Get");

            if (userGroupParameters == null)
                throw new ArgumentNullException(nameof(userGroupParameters));

            if ((userGroupParameters.UserGroupID == null) || (userGroupParameters.UserGroupID == Guid.Empty))
                throw new ArgumentException("Get. User Group ID cannot be null or empty!");

            try
            {
                var userGroupSqlParameters = SqlParameterTools.CreateSqlParameters<UserGroupParameters>(userGroupParameters, OperationKeyTypes.Get);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Get);
                var parameters = new SqlParameter[userGroupSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userGroupSqlParameters, parameters, userGroupSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userGroupSqlParameters.Length, resultSqlParameters.Length);

                var newUserGroup = _databaseManager.ExecuteTransaction<UserGroup>("PROC_GETUSERGROUP", CommandType.StoredProcedure, parameters, UserGroupPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<UserGroup>(resultParamOut, errorMessageOut, newUserGroup);

                Logger.Log.InfoFormat("UserGroupDataProvider. Get. User Group[GroupName: '{0}'] get operation is successful",
                    userGroupParameters.GroupName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Populate a User Group object
        /// </summary>
        /// <param name="reader">SqlDataReader object</param>
        /// <returns></returns>
        private UserGroup UserGroupPopulator(SqlDataReader reader)
        {
            Logger.Log.Debug("UserGroupDataProvider. UserGroupPopulator");

            try
            {
                var userGroup = new UserGroup();
                userGroup.UserGroupID = reader.GetDataType<Guid>("USERGROUPID");
                userGroup.GroupName = reader.GetDataType<string>("GROUPNAME");
                userGroup.GroupNameTranslation = reader.GetDataType<string>("TEXT");
                userGroup.GroupDescription = reader.GetDataType<string>("GROUPDESCRIPTION");
                userGroup.Blocked = reader.GetDataType<bool>("BLOCKED");
                return userGroup;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get List of User Groups from the table
        /// </summary>
        /// <param name="userGroupParameters">User Group parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<List<UserGroup>> GetList(UserGroupParameters userGroupParameters)
        {
            Logger.Log.Debug("UserGroupDataProvider. GetList");

            try
            {
                var userGroupSqlParameters = SqlParameterTools.CreateSqlParameters<UserGroupParameters>(userGroupParameters, OperationKeyTypes.GetList);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.GetList);
                var parameters = new SqlParameter[userGroupSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userGroupSqlParameters, parameters, userGroupSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userGroupSqlParameters.Length, resultSqlParameters.Length);

                var userGroupList = _databaseManager.ExecuteListTransaction<UserGroup>("PROC_GETUSERGROUPLIST", CommandType.StoredProcedure, parameters, UserGroupPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<List<UserGroup>>(resultParamOut, errorMessageOut, userGroupList);

                Logger.Log.InfoFormat("UserGroupDataProvider. GetList. Get List with User Groups operation is successful");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Delete a User Group from the table
        /// </summary>
        /// <param name="userGroupParameters">User Group parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult Delete(UserGroupParameters userGroupParameters)
        {
            Logger.Log.Debug("UserGroupDataProvider. Delete");

            if (userGroupParameters == null)
                throw new ArgumentNullException(nameof(userGroupParameters));

            if ((userGroupParameters.UserGroupID == null) || (userGroupParameters.UserGroupID == Guid.Empty))
                throw new ArgumentException("Delete. User Group ID cannot be null or empty!");

            try
            {
                var userGroupSqlParameters = SqlParameterTools.CreateSqlParameters<UserGroupParameters>(userGroupParameters, OperationKeyTypes.Delete);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Delete);
                var parameters = new SqlParameter[userGroupSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userGroupSqlParameters, parameters, userGroupSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userGroupSqlParameters.Length, resultSqlParameters.Length);

                _databaseManager.ExecuteNonQueryTransaction("PROC_DELETEUSERGROUP", CommandType.StoredProcedure, parameters);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult(resultParamOut, errorMessageOut);

                Logger.Log.InfoFormat("UserGroupDataProvider. Delete. The User Group[Login: '{0}'] Delete operation is successful",
                    userGroupParameters.GroupName);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Search the List of User Groups by Parameters
        /// </summary>
        /// <param name="userGroupParameters">User Group parameters object</param>
        /// <returns></returns>
        public DatabaseQueryResult<List<UserGroup>> Search(UserGroupParameters userGroupParameters)
        {
            Logger.Log.Debug("UserGroupDataProvider. Search");

            if (userGroupParameters == null)
                throw new ArgumentNullException(nameof(userGroupParameters));

            try
            {
                var userGroupSqlParameters = SqlParameterTools.CreateSqlParameters<UserGroupParameters>(userGroupParameters, OperationKeyTypes.Search);
                var spResult = new StoredProcedureResult()
                {
                    Result = 0,
                    ErrorNumber = 0,
                    ErrorMessage = string.Empty
                };
                var resultSqlParameters = SqlParameterTools.CreateSqlParameters<StoredProcedureResult>(spResult, OperationKeyTypes.Search);
                var parameters = new SqlParameter[userGroupSqlParameters.Length + resultSqlParameters.Length];
                Array.Copy(userGroupSqlParameters, parameters, userGroupSqlParameters.Length);
                Array.Copy(resultSqlParameters, 0, parameters, userGroupSqlParameters.Length, resultSqlParameters.Length);

                var userGroupList = _databaseManager.ExecuteListTransaction<UserGroup>("PROC_SEARCHUSERGROUPS", CommandType.StoredProcedure, parameters, UserGroupPopulator);

                spResult = SqlParameterTools.GetObjectFromSqlParameters<StoredProcedureResult>(parameters);
                var resultParamOut = (DatabaseQueryResultTypes)spResult.Result;
                var errorMessageOut = spResult.ErrorMessage as string ?? string.Empty;
                var result = new DatabaseQueryResult<List<UserGroup>>(resultParamOut, errorMessageOut, userGroupList);

                Logger.Log.InfoFormat("UserGroupDataProvider. Search. The Search operation is successful",
                    userGroupParameters.GroupName);
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
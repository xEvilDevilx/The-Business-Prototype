using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

using BP.DataLayer.BusinessObjects.Users;
using BP.DataLayer.Databases.Base.Enums;
using BP.DataLayer.DataProviders;
using BP.DataLayer.DataProviders.Users;
using BP.DataLayer.Interfaces;
using BP.DataService.Base;
using BP.DataService.Common;
using BP.DataService.Plugins.Interfaces;
using BP.SDK.Base.Enums;

namespace BP.DataService.Plugins.Users
{
    /// <summary>
    /// Implements User Host Plugin functionality
    /// 
    /// 2018/10/01 - Created, VTyagunov
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, IncludeExceptionDetailInFaults = true,
        InstanceContextMode = InstanceContextMode.Single)]
    public class UserContract : DataServiceHostPlugin<IUserPlugin>, IUserPlugin
    {
        #region Users

        /// <summary>
        /// Use for Save User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> SaveUser(UserParameters userParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<User, UserParameters> dataprovider = new UserDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Add(userParameters);
                DataContractQueryResult result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult(ResultStateTypes.Success, (int)dbResult.QueryResultType);
                return result;
            });
        }

        /// <summary>
        /// Use for Update User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> UpdateUser(UserParameters userParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<User, UserParameters> dataprovider = new UserDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Update(userParameters);
                DataContractQueryResult result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult(ResultStateTypes.Success, (int)dbResult.QueryResultType);
                return result;
            });
        }

        /// <summary>
        /// Use for Get User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult<User>> GetUser(UserParameters userParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<User, UserParameters> dataprovider = new UserDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Get(userParameters);
                DataContractQueryResult<User> result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult<User>(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult<User>(ResultStateTypes.Success, (int)dbResult.QueryResultType, string.Empty, dbResult.ResultObject);
                return result;
            });
        }

        /// <summary>
        /// Use for Get User List
        /// </summary>
        /// <returns></returns>
        public async Task<DataContractQueryResult<List<User>>> GetUserList()
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<User, UserParameters> dataprovider = new UserDataProvider(ConnectionSettings);
                var dbResult = dataprovider.GetList(new UserParameters());
                DataContractQueryResult<List<User>> result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult<List<User>>(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult<List<User>>(ResultStateTypes.Success, (int)dbResult.QueryResultType, string.Empty, dbResult.ResultObject);
                return result;
            });
        }

        /// <summary>
        /// Use for Delete User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> DeleteUser(UserParameters userParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<User, UserParameters> dataprovider = new UserDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Delete(userParameters);
                DataContractQueryResult result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult(ResultStateTypes.Success, (int)dbResult.QueryResultType);
                return result;
            });
        }

        /// <summary>
        /// Use for Search User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult<List<User>>> SearchUser(UserParameters userParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<User, UserParameters> dataprovider = new UserDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Search(userParameters);
                DataContractQueryResult<List<User>> result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult<List<User>>(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult<List<User>>(ResultStateTypes.Success, (int)dbResult.QueryResultType, string.Empty, dbResult.ResultObject);
                return result;
            });
        }

        #endregion

        #region User Groups

        /// <summary>
        /// Use for Save User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> SaveUserGroup(UserGroupParameters userGroupParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<UserGroup, UserGroupParameters> dataprovider = new UserGroupDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Add(userGroupParameters);
                DataContractQueryResult result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult(ResultStateTypes.Success, (int)dbResult.QueryResultType);
                return result;
            });
        }

        /// <summary>
        /// Use for Update User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> UpdateUserGroup(UserGroupParameters userGroupParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<UserGroup, UserGroupParameters> dataprovider = new UserGroupDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Update(userGroupParameters);
                DataContractQueryResult result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult(ResultStateTypes.Success, (int)dbResult.QueryResultType);
                return result;
            });
        }

        /// <summary>
        /// Use for Get User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult<UserGroup>> GetUserGroup(UserGroupParameters userGroupParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<UserGroup, UserGroupParameters> dataprovider = new UserGroupDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Get(userGroupParameters);
                DataContractQueryResult<UserGroup> result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult<UserGroup>(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult<UserGroup>(ResultStateTypes.Success, (int)dbResult.QueryResultType, string.Empty, dbResult.ResultObject);
                return result;
            });
        }

        /// <summary>
        /// Use for Get User Group List
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult<List<UserGroup>>> GetUserGroupList(UserGroupParameters userGroupParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<UserGroup, UserGroupParameters> dataprovider = new UserGroupDataProvider(ConnectionSettings);
                var dbResult = dataprovider.GetList(userGroupParameters);
                DataContractQueryResult<List<UserGroup>> result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult<List<UserGroup>>(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult<List<UserGroup>>(ResultStateTypes.Success, (int)dbResult.QueryResultType, string.Empty, dbResult.ResultObject);
                return result;
            });
        }

        /// <summary>
        /// Use for Delete User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> DeleteUserGroup(UserGroupParameters userGroupParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<UserGroup, UserGroupParameters> dataprovider = new UserGroupDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Delete(userGroupParameters);
                DataContractQueryResult result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult(ResultStateTypes.Success, (int)dbResult.QueryResultType);
                return result;
            });
        }

        /// <summary>
        /// Use for Search User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult<List<UserGroup>>> SearchUserGroup(UserGroupParameters userGroupParameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                IDataProvider<UserGroup, UserGroupParameters> dataprovider = new UserGroupDataProvider(ConnectionSettings);
                var dbResult = dataprovider.Search(userGroupParameters);
                DataContractQueryResult<List<UserGroup>> result = null;
                if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                    result = new DataContractQueryResult<List<UserGroup>>(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                else result = new DataContractQueryResult<List<UserGroup>>(ResultStateTypes.Success, (int)dbResult.QueryResultType, string.Empty, dbResult.ResultObject);
                return result;
            });
        }

        #endregion

        #region User Rights

        #region Additional functionality

        /// <summary>
        /// Use for Change User Right
        /// </summary>
        /// <param name="parameters">User Right Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> ChangeUserRight(UserRightParameters parameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (IDataProviderManager dpManager = new DataProviderManager(ConnectionSettings))
                {
                    var dbResult = dpManager.ExecuteNoneQuerySP(parameters.SPNames[UserRightParameterTypes.ChangeUserRight], parameters,
                        UserRightParameterTypes.ChangeUserRight);
                    DataContractQueryResult result = null;
                    if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                        result = new DataContractQueryResult(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                    else result = new DataContractQueryResult(ResultStateTypes.Success, (int)dbResult.QueryResultType);
                    return result;
                }
            });
        }

        /// <summary>
        /// Use for Change User Group Right
        /// </summary>
        /// <param name="parameters">User Right Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> ChangeUserGroupRight(UserRightParameters parameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (IDataProviderManager dpManager = new DataProviderManager(ConnectionSettings))
                {
                    var dbResult = dpManager.ExecuteNoneQuerySP(parameters.SPNames[UserRightParameterTypes.ChangeUserGroupRight], parameters,
                        UserRightParameterTypes.ChangeUserGroupRight);
                    DataContractQueryResult result = null;
                    if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                        result = new DataContractQueryResult(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                    else result = new DataContractQueryResult(ResultStateTypes.Success, (int)dbResult.QueryResultType);
                    return result;
                }
            });
        }

        /// <summary>
        /// Use for Get User Right Status type
        /// </summary>
        /// <param name="parameters">User Right Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult<RightStatusTypes>> GetUserRightStatusType(UserRightParameters parameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (IDataProviderManager dpManager = new DataProviderManager(ConnectionSettings))
                {
                    var dbResult = dpManager.ExecuteScalarSP(parameters.SPNames[UserRightParameterTypes.GetUserRightsStatus], parameters,
                        UserRightParameterTypes.GetUserRightsStatus);
                    DataContractQueryResult<RightStatusTypes> result = null;
                    if (dbResult.QueryResultType != DatabaseQueryResultTypes.Success)
                        result = new DataContractQueryResult<RightStatusTypes>(ResultStateTypes.Failed, (int)dbResult.QueryResultType, dbResult.ResultMessage);
                    else result = new DataContractQueryResult<RightStatusTypes>(ResultStateTypes.Success, (int)dbResult.QueryResultType, dbResult.ResultMessage,
                        (RightStatusTypes)(byte)dbResult.ResultObject);
                    return result;
                }
            });
        }

        /// <summary>
        /// Use for Get User Rights
        /// </summary>
        /// <param name="parameters">User Right Parameters</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult<Dictionary<Right, RightStatusTypes>>> GetUserRights(UserRightParameters parameters)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (IDataProviderManager dpManager = new DataProviderManager(ConnectionSettings))
                {
                    //var dbResult = dpManager.ExecuteListSP(parameters.SPNames[UserRightParameterTypes.GetUserRightsStatus], parameters,
                    //    UserRightParameterTypes.GetUserRightsStatus);
                    DataContractQueryResult<Dictionary<Right, RightStatusTypes>> result = null;
                    return result;
                }
            });
        }

        #endregion

        #endregion
    }
}
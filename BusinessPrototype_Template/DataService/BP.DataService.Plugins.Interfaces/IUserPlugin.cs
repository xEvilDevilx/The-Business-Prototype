using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

using BP.DataLayer.BusinessObjects.Users;
using BP.DataService.Base;
using BP.DataService.Interfaces;

namespace BP.DataService.Plugins.Interfaces
{
    /// <summary>
    /// Presents User Plugin functionality interface
    /// 
    /// 2018/10/01 - Created, VTyagunov
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IUserPlugin : IDataServicePlugin
    {
        #region Users

        /// <summary>
        /// Use for Save User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> SaveUser(UserParameters userParameters);

        /// <summary>
        /// Use for Update User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> UpdateUser(UserParameters userParameters);

        /// <summary>
        /// Use for Get User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult<User>> GetUser(UserParameters userParameters);

        /// <summary>
        /// Use for Get User List
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult<List<User>>> GetUserList();

        /// <summary>
        /// Use for Delete User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> DeleteUser(UserParameters userParameters);

        /// <summary>
        /// Use for Search User
        /// </summary>
        /// <param name="userParameters">User Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult<List<User>>> SearchUser(UserParameters userParameters);

        #endregion

        #region User Groups

        /// <summary>
        /// Use for Save User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> SaveUserGroup(UserGroupParameters userGroupParameters);

        /// <summary>
        /// Use for Update User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> UpdateUserGroup(UserGroupParameters userGroupParameters);

        /// <summary>
        /// Use for Get User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult<UserGroup>> GetUserGroup(UserGroupParameters userGroupParameters);

        /// <summary>
        /// Use for Get User Group List
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult<List<UserGroup>>> GetUserGroupList(UserGroupParameters userGroupParameters);

        /// <summary>
        /// Use for Delete User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> DeleteUserGroup(UserGroupParameters userGroupParameters);

        /// <summary>
        /// Use for Search User Group
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult<List<UserGroup>>> SearchUserGroup(UserGroupParameters userGroupParameters);

        #endregion

        #region User Rights

        #region Additional functionality

        /// <summary>
        /// Use for Change User Right
        /// </summary>
        /// <param name="parameters">User Right Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> ChangeUserRight(UserRightParameters parameters);

        /// <summary>
        /// Use for Change User Group Right
        /// </summary>
        /// <param name="parameters">User Right Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> ChangeUserGroupRight(UserRightParameters parameters);

        /// <summary>
        /// Use for Get User Right Status type
        /// </summary>
        /// <param name="parameters">User Right Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult<RightStatusTypes>> GetUserRightStatusType(UserRightParameters parameters);

        /// <summary>
        /// Use for Get User Rights
        /// </summary>
        /// <param name="parameters">User Right Parameters</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult<Dictionary<Right, RightStatusTypes>>> GetUserRights(UserRightParameters parameters);

        #endregion

        #endregion
    }
}
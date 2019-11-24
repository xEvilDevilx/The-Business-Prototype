using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BP.DataLayer.BusinessObjects.Users;
using BP.DataService.Base;
using BP.DataService.Common;
using BP.DataService.Plugins.Interfaces;
using BP.DataService.WinService.Channels;
using BP.SDK.Extensions;

namespace BP.DataService.TestClientApp.DebugConsole
{
    /// <summary>
    /// Presents Main Entry Point to app
    /// 
    /// 2018/09/19 - Created, VTyagunov
    /// </summary>
    class Program
    {
        /// <summary>Data Service Client</summary>
        private static DataServiceChannel _dataServiceChannel;
        /// <summary>Collection of Plugin names and codes</summary>
        private static Dictionary<string, string> _pluginNames;

        /// <summary>
        /// Main Entry Point to app
        /// </summary>
        /// <param name="args">Arguments array</param>
        static void Main(string[] args)
        {
            _pluginNames = new Dictionary<string, string>()
            {
                { "Users", "BP.DataService.Plugins.Users.dll" }
            };

            _dataServiceChannel = new DataServiceChannel();
            _dataServiceChannel.Connect();

            Console.WriteLine("Enter one of commands and press 'Enter':");
            Console.WriteLine("'Q' - for quit");
            Console.WriteLine("001 - for send query to Test a Connection");
            Console.WriteLine("002 - for send query to Start a User Plugin");
            Console.WriteLine("003 - for send query to Stop a User Plugin");
            Console.WriteLine("004 - for start a StartSaveUser method");
            Console.WriteLine("005 - for start a StartUpdateUser method");
            Console.WriteLine("006 - for start a StartGetUser method");
            Console.WriteLine("007 - for start a StartGetUserList method");
            Console.WriteLine("008 - for start a StartDeleteUser method");
            Console.WriteLine("009 - for start a StartSearchUser method");
            Console.WriteLine("010 - for start a StartAddUserGroup method");
            Console.WriteLine("011 - for start a StartUpdateUserGroup method");
            Console.WriteLine("012 - for start a StartGetUserGroup method");
            Console.WriteLine("013 - for start a StartGetUserGroupList method");
            Console.WriteLine("014 - for start a StartDeleteUserGroup method");
            Console.WriteLine("015 - for start a StartSearchUserGroup method");
            Console.WriteLine("016 - for start a StartChangeUserRight method");
            Console.WriteLine("017 - for start a StartChangeUserGroupRight method");
            Console.WriteLine("018 - for start a StartGetUserRightStatusType method");
            string str = string.Empty;
            string resultCodeText = string.Empty;

            while (str != "Q")
            {
                str = Console.ReadLine();

                switch (str)
                {
                    case "001":
                        TestConnection();
                        break;
                    case "002":
                        StartPlugin();
                        break;
                    case "003":
                        StopPlugin();                        
                        break;                    
                    case "004":
                        StartSaveUser();                        
                        break;
                    case "005":
                        StartUpdateUser();                        
                        break;
                    case "006":
                        StartGetUser();                        
                        break;
                    case "007":
                        StartGetUserList();                        
                        break;
                    case "008":
                        StartDeleteUser();                        
                        break;
                    case "009":
                        StartSearchUser();                        
                        break;
                    case "010":
                        StartAddUserGroup();                        
                        break;
                    case "011":
                        StartUpdateUserGroup();                        
                        break;
                    case "012":
                        StartGetUserGroup();                        
                        break;
                    case "013":
                        StartGetUserGroupList();                        
                        break;
                    case "014":
                        StartDeleteUserGroup();                        
                        break;
                    case "015":
                        StartSearchUserGroup();                        
                        break;
                    case "016": // ChangeUserRight
                        StartChangeUserRight();                        
                        break;
                    case "017": // ChangeUserGroupRight
                        StartChangeUserGroupRight();                        
                        break;
                    case "018": // GetUserRightStatusType
                        StartGetUserRightStatusType();                        
                        break;
                    default:
                        Console.WriteLine("Unknown command! Please try again!");
                        break;
                }
            }

            _dataServiceChannel.Disconnect();
        }

        #region Case Methods

        #region Common Service methods

        private static void TestConnection()
        {
            var testConnectionResult = _dataServiceChannel.CheckConnection();
            Console.WriteLine("TestConnection [Result: {0}]", testConnectionResult.Result);
        }

        private static void StartPlugin()
        {
            var startPluginResult = _dataServiceChannel.StartPlugin(_pluginNames["Users"]);
            Console.WriteLine("StartPlugin [ResultState: {0}] - [ResultMessage: {1}]", startPluginResult.Result.ResultState,
                startPluginResult.Result.ResultMessage);
        }

        private static void StopPlugin()
        {
            var stopPluginResult = _dataServiceChannel.StopPlugin(_pluginNames["Users"]);
            Console.WriteLine("StopPlugin [ResultState: {0}] - [ResultMessage: {1}]", stopPluginResult.Result.ResultState,
                stopPluginResult.Result.ResultMessage);
        }

        #endregion

        #region Start User methods

        private static void StartSaveUser()
        {
            var resultCodeText = string.Empty;
            var savePassword = "12345";
            var saveSecurePassword = savePassword.ConvertToSecureString();
            var newUser = new UserParameters()
            {
                Login = "test",
                Password = saveSecurePassword,
                FirstName = "Vladimir",
                MiddleName = "Urievich",
                LastName = "Tyagunov",
                Rights = new Right(),
                Blocked = false
            };
            var saveUserQueryResult = SaveUser(newUser);
            var saveUserResult = saveUserQueryResult.Result;

            if (saveUserResult.ResultCode == 2)
                resultCodeText = "Current user already exists!";
            Console.WriteLine("SaveUser[Login: {0}]: \n[Result: {1}] \n[Result Message: {2}] \n[Result Code Message: {3}]",
                newUser.Login, saveUserResult.ResultState, saveUserResult.ResultMessage, resultCodeText);
        }

        private static void StartUpdateUser()
        {
            var resultCodeText = string.Empty;
            var updatePassword = "012345";
            var updateSecurePassword = updatePassword.ConvertToSecureString();
            var updateUser = new UserParameters()
            {
                ID = Guid.Parse("C4A952BD-DFEE-4B3D-95C6-13936577F6B7"),
                Login = "2test2",
                Password = updateSecurePassword,
                FirstName = "First",
                MiddleName = "Middle",
                LastName = "Last",
                Rights = new Right(),
                LastLoginDateTime = DateTime.Now,
                Blocked = true
            };
            var updateUserQueryResult = UpdateUser(updateUser);
            var updateUserResult = updateUserQueryResult.Result;

            if (updateUserResult.ResultCode == 3)
                resultCodeText = "User is not exists!";
            Console.WriteLine("UpdateUser[Login: {0}]: \n[Result: {1}] \n[Result Message: {2}] \n[Result Code Message: {3}]",
                updateUser.Login, updateUserResult.ResultState, updateUserResult.ResultMessage, resultCodeText);
        }

        private static void StartGetUser()
        {
            var userIDForGet = Guid.Parse("80C036F5-8272-4FB5-8593-F3122B096E7F");
            var userGetParameters = new UserParameters
            {
                ID = userIDForGet
            };
            var userGetResult = GetUser(userGetParameters);
            var user = userGetResult.Result.ResultObject;
            Console.WriteLine(@"Get User[Result: {0}] [Result Message: {1}] [Result Code Message: {2}]: \n[UserID: {3}] \n[UserGroupID: {4}]
                                            \n[Login: {5}] \n[Password: {6}] \n[FirstName: {7}] \n[MiddleName: {8}] \n[LastName: {9}] \n[CreateDateTime: {10}] 
                                            \n[LastLoginDateTime: {11}] \n[Blocked: {12}]", userGetResult.Result.ResultState, userGetResult.Result.ResultMessage,
                                userGetResult.Result.ResultCode, user.ID, user.UserGroupID, user.Login, user.Password, user.FirstName, user.MiddleName,
                                user.LastName, user.CreateDateTime, user.LastLoginDateTime != null ? user.LastLoginDateTime.ToString() : string.Empty, user.Blocked);
        }

        private static void StartGetUserList()
        {
            var userListResult = GetUserList();
            var userList = userListResult.Result.ResultObject;

            Console.WriteLine("Get User[Result: {0}] [Result Message: {1}] [Result Code Message: {2}]\n", userListResult.Result.ResultState,
                userListResult.Result.ResultMessage, userListResult.Result.ResultCode);
            for (int i = 0; i < userList.Count; i++)
            {
                Console.WriteLine(@"GetList User[UserID: {0}] \n[UserGroupID: {1}] \n[Login: {2}] \n[Password: {3}] \n[FirstName: {4}] \n[MiddleName: {5}] 
                                            \n[LastName: {6}] \n[CreateDateTime: {7}] \n[LastLoginDateTime: {8}] \n[Blocked: {9}]", userList[i].ID, userList[i].UserGroupID,
                                userList[i].Login, userList[i].Password, userList[i].FirstName, userList[i].MiddleName, userList[i].LastName,
                                userList[i].CreateDateTime, userList[i].LastLoginDateTime != null ? userList[i].LastLoginDateTime.ToString() : string.Empty,
                                userList[i].Blocked);
                Console.WriteLine();
            }
        }

        private static void StartDeleteUser()
        {
            var userIDForDelete = Guid.Parse("80C036F5-8272-4FB5-8593-F3122B096E7F");
            var userDeleteParameters = new UserParameters
            {
                ID = userIDForDelete
            };
            var userDeleteResult = DeleteUser(userDeleteParameters);
            Console.WriteLine(@"Delete User[Result: {0}] [Result Message: {1}] [Result Code Message: {2}]", userDeleteResult.Result.ResultState, userDeleteResult.Result.ResultMessage,
                                userDeleteResult.Result.ResultCode);
        }

        private static void StartSearchUser()
        {
            var resultCodeText = string.Empty;
            var searchUser = new UserParameters()
            {
                //ID = Guid.Parse("80C036F5-8272-4FB5-8593-F3122B096E7F"),
                //UserGroupID = Guid.Parse("A2E0E67A-E75F-4632-ACC8-4393E275F5B8"),
                //Login = "admin",
                //FirstName = "Ad",
                //MiddleName = "M",
                //LastName = "iN",                            
                //CreateDateTimeFrom = DateTime.Parse("2018-10-17 00:00:00.000"),
                //CreateDateTimeTo = DateTime.Parse("2018-10-21 00:00:00.000"),
                //LastLoginDateTimeFrom = DateTime.Parse("2018-11-09 10:45:58.166"),
                //LastLoginDateTimeTo = DateTime.Parse("2018-11-09 10:45:58.168"),
                //Rights = new UserRights(),
                //Blocked = false
            };
            var searchUserQueryResult = SearchUser(searchUser);
            var searchUserResult = searchUserQueryResult.Result;

            if (searchUserResult.ResultCode == 3)
                resultCodeText = "User is not exists!";
            Console.WriteLine("SearchUser[Login: {0}]: \n[Result: {1}] \n[Result Message: {2}] \n[Result Code Message: {3}]",
                searchUser.Login, searchUserResult.ResultState, searchUserResult.ResultMessage, resultCodeText);
        }

        #endregion

        #region Start User Group methods

        private static void StartAddUserGroup()
        {
            var resultCodeText = string.Empty;
            var newUserGroup = new UserGroupParameters()
            {
                GroupName = "TestUG",
                GroupDescription = "TestDescr",
                Blocked = true,
                LanguageCode = "ENG",
                GroupNameTranslation = "Test User Group"
            };
            var saveUserGroupQueryResult = SaveUserGroup(newUserGroup);
            var saveUserGroupResult = saveUserGroupQueryResult.Result;

            if (saveUserGroupResult.ResultCode == 2)
                resultCodeText = "Current user group already exists!";
            Console.WriteLine("Save User Group Result: {0}", saveUserGroupResult.ResultState);
        }

        private static void StartUpdateUserGroup()
        {
            var resultCodeText = string.Empty;
            var updateUserGroup = new UserGroupParameters()
            {
                UserGroupID = Guid.Parse("23A3D18C-7C58-4F5F-949E-B09693BC0761"),
                GroupName = "Updated Name 2",
                GroupDescription = "Updated Descr 2",
                Blocked = false,
                LanguageCode = "RUS",
                GroupNameTranslation = "Обновлено!"
            };
            var updateUserGroupQueryResult = UpdateUserGroup(updateUserGroup);
            var updateUserGroupResult = updateUserGroupQueryResult.Result;

            Console.WriteLine("Update User Group Result: {0}", updateUserGroupResult.ResultState);
        }

        private static void StartGetUserGroup()
        {
            var userGroupIDForGet = Guid.Parse("A2E0E67A-E75F-4632-ACC8-4393E275F5B8");
            var userGroupGetParameters = new UserGroupParameters
            {
                UserGroupID = userGroupIDForGet,
                LanguageCode = "ENG"
            };
            var userGroupGetResult = GetUserGroup(userGroupGetParameters);
            var userGroup = userGroupGetResult.Result.ResultObject;
            Console.WriteLine("Get User Group Result: {0}, User Group Name: {1}", userGroupGetResult.Result.ResultState, userGroup.GroupName);
        }

        private static void StartGetUserGroupList()
        {
            var userGroupGetListParameters = new UserGroupParameters
            {
                LanguageCode = "ENG"
            };
            var userGroupListResult = GetUserGroupList(userGroupGetListParameters);
            var userGroupList = userGroupListResult.Result.ResultObject;

            Console.WriteLine("Get User Group List Result: {0}, Count: {1}", userGroupListResult.Result.ResultState, userGroupList.Count);
        }

        private static void StartDeleteUserGroup()
        {
            var userGroupIDForDelete = Guid.Parse("23A3D18C-7C58-4F5F-949E-B09693BC0761");
            var userGroupDeleteParameters = new UserGroupParameters
            {
                UserGroupID = userGroupIDForDelete
            };
            var userGroupDeleteResult = DeleteUserGroup(userGroupDeleteParameters);
            Console.WriteLine("Delete User Group Result: {0}", userGroupDeleteResult.Result.ResultState);
        }

        private static void StartSearchUserGroup()
        {
            var searchUserGroup = new UserGroupParameters()
            {
                //UserGroupID = Guid.Parse("A2E0E67A-E75F-4632-ACC8-4393E275F5B8"),
                //GroupName = "Administrators",
                LanguageCode = "RUS",
                Blocked = false
            };
            var searchUserGroupQueryResult = SearchUserGroup(searchUserGroup);
            var searchUserGroupResult = searchUserGroupQueryResult.Result;

            Console.WriteLine("Search User Group Result: {0}", searchUserGroupResult.ResultState);
        }

        #endregion

        #region Start User Right methods

        #region Additional Right methods

        private static void StartChangeUserRight()
        {
            var userID = Guid.Parse("C894A4C2-9C21-4341-BE4C-DF0DC8A8A56E");
            var rightID = Guid.Parse("D7E16F52-60FD-4039-A48F-AF999A4FAF12");
            var rightStatusType = RightStatusTypes.Allow;
            var parameters = new UserRightParameters()
            {
                UserID = userID,
                RightID = rightID,
                RightStatusTinyint = (byte)rightStatusType
            };
            var changeUserRightQueryResult = ChangeUserRight(parameters);
            var changeUserRightResult = changeUserRightQueryResult.Result;

            Console.WriteLine("Change User Right Result: {0}", changeUserRightResult.ResultState);
        }

        private static void StartChangeUserGroupRight()
        {
            var userGroupID = Guid.Parse("A2E0E67A-E75F-4632-ACC8-4393E275F5B8");
            var groupRightID = Guid.Parse("DFF228B0-EE12-443A-BD93-43D28DBE858A");
            var groupRightStatusType = true; // RightStatusTypes.Allow;
            var groupRightParameters = new UserRightParameters()
            {
                UserGroupID = userGroupID,
                RightID = groupRightID,
                RightStatusBit = groupRightStatusType
            };
            var changeUserGroupRightQueryResult = ChangeUserGroupRight(groupRightParameters);
            var changeUserGroupRightResult = changeUserGroupRightQueryResult.Result;

            Console.WriteLine("Change User Group Right Result: {0}", changeUserGroupRightResult.ResultState);
        }

        private static void StartGetUserRightStatusType()
        {
            var userID = Guid.Parse("C894A4C2-9C21-4341-BE4C-DF0DC8A8A56E");
            var rightID = Guid.Parse("D7E16F52-60FD-4039-A48F-AF999A4FAF12");
            var groupRightParameters = new UserRightParameters()
            {
                UserID = userID,
                RightID = rightID
            };
            var queryResult = GetUserRightStatusType(groupRightParameters);
            var result = queryResult.Result;

            Console.WriteLine("GetUserRightStatusType Result: {0}", result.ResultState);
        }

        #endregion

        #endregion

        #endregion

        #region Users

        /// <summary>
        /// Use for Execute SaveUser method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userParameters">User Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult> SaveUser(UserParameters userParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult>(async (IUserPlugin userPlugin) => await userPlugin.SaveUser(userParameters));
        }

        /// <summary>
        /// Use for Execute UpdateUser method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userParameters">User Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult> UpdateUser(UserParameters userParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult>(async (IUserPlugin userPlugin) => await userPlugin.UpdateUser(userParameters));
        }

        /// <summary>
        /// Use for Execute GetUser method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userParameters">User Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult<User>> GetUser(UserParameters userParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult<User>>(async (IUserPlugin userPlugin) => await userPlugin.GetUser(userParameters));
        }

        /// <summary>
        /// Use for Execute GetUserList method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <returns></returns>
        private static async Task<DataContractQueryResult<List<User>>> GetUserList()
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult<List<User>>>(async (IUserPlugin userPlugin) => await userPlugin.GetUserList());
        }

        /// <summary>
        /// Use for Execute DeleteUser method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userParameters">User Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult> DeleteUser(UserParameters userParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult>(async (IUserPlugin userPlugin) => await userPlugin.DeleteUser(userParameters));
        }

        /// <summary>
        /// Use for Execute SearchUser method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userParameters">User Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult<List<User>>> SearchUser(UserParameters userParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult<List<User>>>(async (IUserPlugin userPlugin) => await userPlugin.SearchUser(userParameters));
        }

        #endregion

        #region User Groups

        /// <summary>
        /// Use for Execute SaveUserGroup method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult> SaveUserGroup(UserGroupParameters userGroupParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult>(async (IUserPlugin userPlugin) => await userPlugin.SaveUserGroup(userGroupParameters));
        }

        /// <summary>
        /// Use for Execute UpdateUserGroup method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult> UpdateUserGroup(UserGroupParameters userGroupParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult>(async (IUserPlugin userPlugin) => await userPlugin.UpdateUserGroup(userGroupParameters));
        }

        /// <summary>
        /// Use for Execute GetUserGroup method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult<UserGroup>> GetUserGroup(UserGroupParameters userGroupParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult<UserGroup>>(async (IUserPlugin userPlugin) => await userPlugin.GetUserGroup(userGroupParameters));
        }

        /// <summary>
        /// Use for Execute GetUserGroupList method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult<List<UserGroup>>> GetUserGroupList(UserGroupParameters userGroupParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult<List<UserGroup>>>(async (IUserPlugin userPlugin) => await userPlugin.GetUserGroupList(userGroupParameters));
        }

        /// <summary>
        /// Use for Execute DeleteUserGroup method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult> DeleteUserGroup(UserGroupParameters userGroupParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult>(async (IUserPlugin userPlugin) => await userPlugin.DeleteUserGroup(userGroupParameters));
        }

        /// <summary>
        /// Use for Execute SearchUserGroup method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Group Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult<List<UserGroup>>> SearchUserGroup(UserGroupParameters userGroupParameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult<List<UserGroup>>>(async (IUserPlugin userPlugin) => await userPlugin.SearchUserGroup(userGroupParameters));
        }

        #endregion

        #region UserRights

        #region Additional

        /// <summary>
        /// Use for Execute ChangeUserRight method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Right Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult> ChangeUserRight(UserRightParameters parameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult>(async (IUserPlugin userPlugin) => await userPlugin.ChangeUserRight(parameters));
        }

        /// <summary>
        /// Use for Execute ChangeUserGroupRight method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Group Right Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult> ChangeUserGroupRight(UserRightParameters parameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult>(async (IUserPlugin userPlugin) => await userPlugin.ChangeUserGroupRight(parameters));
        }

        /// <summary>
        /// Use for Execute GetUserRightStatusType method of the IUserPlugin interface to Data Service
        /// </summary>
        /// <param name="userGroupParameters">User Group Right Parameters object</param>
        /// <returns></returns>
        private static async Task<DataContractQueryResult<RightStatusTypes>> GetUserRightStatusType(UserRightParameters parameters)
        {
            return await DataServiceClient.Execute<IUserPlugin, DataContractQueryResult<RightStatusTypes>>(async (IUserPlugin userPlugin) => await userPlugin.GetUserRightStatusType(parameters));
        }

        #endregion

        #endregion
    }
}
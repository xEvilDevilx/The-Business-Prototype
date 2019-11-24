using System;
using System.Collections.Generic;
using System.Data;

using BP.DataLayer.Base.Attributes;

namespace BP.DataLayer.BusinessObjects.Users
{
    /// <summary>
    /// Presents User Right Parameters data object
    /// 
    /// 2018/11/20 - Created, VTyagunov
    /// </summary>
    public class UserRightParameters
    {
        /// <summary>
        /// Stored Procedure Names collection
        /// </summary>
        public Dictionary<UserRightParameterTypes, string> SPNames = new Dictionary<UserRightParameterTypes, string>()
        {
            { UserRightParameterTypes.ChangeUserGroupRight, "PROC_CHANGEUSERGROUPRIGHT" },
            { UserRightParameterTypes.ChangeUserRight, "PROC_CHANGEUSERRIGHT" },
            { UserRightParameterTypes.GetUserRights, "PROC_GETUSERRIGHTS" },
            { UserRightParameterTypes.GetUserRightsStatus, "PROC_GETUSERRIGHTSTATUS" },
        };

        /// <summary>
        /// User ID
        /// </summary>
        [OperationsEnum(
            UserRightParameterTypes.ChangeUserRight |
            UserRightParameterTypes.GetUserRightsStatus |
            UserRightParameterTypes.GetUserRights)]
        [Parameters("@UserID", SqlDbType.UniqueIdentifier)]        
        public Guid? UserID { get; set; }

        /// <summary>
        /// User Group ID
        /// </summary>
        [OperationsEnum(UserRightParameterTypes.ChangeUserGroupRight)]
        [Parameters("@UserGroupID", SqlDbType.UniqueIdentifier)]
        public Guid? UserGroupID { get; set; }

        /// <summary>
        /// Right ID
        /// </summary>
        [OperationsEnum(
            UserRightParameterTypes.ChangeUserRight |
            UserRightParameterTypes.ChangeUserGroupRight |
            UserRightParameterTypes.GetUserRightsStatus)]
        [Parameters("@RightID", SqlDbType.UniqueIdentifier)]
        public Guid? RightID { get; set; }

        /// <summary>
        /// Language Code
        /// </summary>
        [OperationsEnum(UserRightParameterTypes.GetUserRights)]
        [Parameters("@LanguageCode", SqlDbType.NVarChar)]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Right Status with a Bit type
        /// </summary>
        [OperationsEnum(UserRightParameterTypes.ChangeUserGroupRight)]
        [Parameters("@RightStatus", SqlDbType.Bit)]
        public bool RightStatusBit { get; set; }

        /// <summary>
        /// Right Status with a Tinyint type
        /// </summary>
        [OperationsEnum(UserRightParameterTypes.ChangeUserRight)]
        [Parameters("@RightStatus", SqlDbType.TinyInt)]
        public byte RightStatusTinyint { get; set; }
    }
}
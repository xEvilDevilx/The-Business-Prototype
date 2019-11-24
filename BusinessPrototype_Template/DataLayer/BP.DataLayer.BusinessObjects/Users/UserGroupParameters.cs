using System;
using System.Data;

using BP.DataLayer.Base.Attributes;
using BP.DataLayer.Base.Enums;

namespace BP.DataLayer.BusinessObjects.Users
{
    /// <summary>
    /// Presents User Group Parameters data object
    /// 
    /// 2018/11/09 - Created, VTyagunov
    /// </summary>
    public class UserGroupParameters
    {
        /// <summary>
        /// User Group ID
        /// </summary>
        [Operations(
            OperationKeyTypes.Update |
            OperationKeyTypes.Get |
            OperationKeyTypes.Delete |
            OperationKeyTypes.Search)]
        [Parameters("@UserGroupID", SqlDbType.UniqueIdentifier)]
        public Guid? UserGroupID { get; set; }

        /// <summary>
        /// Group Name
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@GroupName", SqlDbType.NVarChar)]
        public string GroupName { get; set; }

        /// <summary>
        /// Group Description
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update)]
        [Parameters("@GroupDescription", SqlDbType.NVarChar)]
        public string GroupDescription { get; set; }

        /// <summary>
        /// Blocked status
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@Blocked", SqlDbType.Bit)]
        public bool Blocked { get; set; }

        /// <summary>
        /// Language Code
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Get |
            OperationKeyTypes.GetList |
            OperationKeyTypes.Search)]
        [Parameters("@LanguageCode", SqlDbType.NVarChar)]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Group Name Translation
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update)]
        [Parameters("@GroupNameTranslation", SqlDbType.NVarChar)]
        public string GroupNameTranslation { get; set; }
    }
}
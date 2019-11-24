using System;

namespace BP.DataLayer.BusinessObjects.Users
{
    /// <summary>
    /// Presents User Group data object
    /// 
    /// 2018/11/09 - Created, VTyagunov
    /// </summary>
    public class UserGroup
    {
        /// <summary>
        /// User Group ID
        /// </summary>
        public Guid UserGroupID { get; set; }

        /// <summary>
        /// Group Name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Group Description
        /// </summary>
        public string GroupDescription { get; set; }

        /// <summary>
        /// Blocked status
        /// </summary>
        public bool Blocked { get; set; }

        /// <summary>
        /// Group Name Translation
        /// </summary>
        public string GroupNameTranslation { get; set; }
    }
}
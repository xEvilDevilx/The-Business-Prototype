using System;
using System.Security;

namespace BP.DataLayer.BusinessObjects.Users
{
    /// <summary>
    /// Presents User data object
    /// 
    /// 2018/10/02 - Created, VTyagunov
    /// </summary>
    public class User
    {
        /// <summary>
        /// User ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// User Group ID
        /// </summary>
        public Guid UserGroupID { get; set; }

        /// <summary>
        /// Login of the User
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password of the User
        /// </summary>
        public SecureString Password { get; set; }

        /// <summary>
        /// Hash of the User Password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// First Name of the User
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Middle Name of the User
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Last Name of the User
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Date and Time of the Creation User
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// Date and Time of the User Last Login
        /// </summary>
        public DateTime? LastLoginDateTime { get; set; }

        /// <summary>
        /// Block status of the User
        /// </summary>
        public bool? Blocked { get; set; }
    }
}
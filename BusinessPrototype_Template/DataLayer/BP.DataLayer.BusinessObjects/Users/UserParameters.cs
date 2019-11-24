using System;
using System.Data;
using System.Security;

using BP.DataLayer.Base.Attributes;
using BP.DataLayer.Base.Enums;

namespace BP.DataLayer.BusinessObjects.Users
{
    /// <summary>
    /// Presents User Parameters data object
    /// 
    /// 2018/11/09 - Created, VTyagunov
    /// </summary>
    public class UserParameters
    {
        /// <summary>
        /// User ID
        /// </summary>
        [Operations(
            OperationKeyTypes.Update |
            OperationKeyTypes.Get |
            OperationKeyTypes.Delete |
            OperationKeyTypes.Search)]
        [Parameters("@UserID", SqlDbType.UniqueIdentifier)]
        public Guid? ID { get; set; }

        /// <summary>
        /// User Group ID
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@UserGroupID", SqlDbType.UniqueIdentifier)]
        public Guid? UserGroupID { get; set; }

        /// <summary>
        /// Login of the User
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@Login", SqlDbType.NVarChar)]
        public string Login { get; set; }

        /// <summary>
        /// Password of the User
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update)]
        [Parameters("@Password", SqlDbType.NVarChar)]
        public SecureString Password { get; set; }

        /// <summary>
        /// First Name of the User
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@FirstName", SqlDbType.NVarChar)]
        public string FirstName { get; set; }

        /// <summary>
        /// Middle Name of the User
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@MiddleName", SqlDbType.NVarChar)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Last Name of the User
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@LastName", SqlDbType.NVarChar)]
        public string LastName { get; set; }

        /// <summary>
        /// Parameter for Date and Time of the Creation User From
        /// </summary>
        [Operations(OperationKeyTypes.Search)]
        [Parameters("@CreateDateTimeFrom", SqlDbType.DateTime)]
        public DateTime? CreateDateTimeFrom { get; set; }

        /// <summary>
        /// Parameter for Date and Time of the Creation User To
        /// </summary>
        [Operations(OperationKeyTypes.Search)]
        [Parameters("@CreateDateTimeTo", SqlDbType.DateTime)]
        public DateTime? CreateDateTimeTo { get; set; }

        /// <summary>
        /// Date and Time of the User Last Login
        /// </summary>
        [Operations(OperationKeyTypes.Update)]
        [Parameters("@LastLoginDateTime", SqlDbType.DateTime)]
        public DateTime? LastLoginDateTime { get; set; }

        /// <summary>
        /// Parameter for Date and Time of the User Last Login From
        /// </summary>
        [Operations(OperationKeyTypes.Search)]
        [Parameters("@LastLoginDateTimeFrom", SqlDbType.DateTime)]
        public DateTime? LastLoginDateTimeFrom { get; set; }

        /// <summary>
        /// Parameter for Date and Time of the User Last Login To
        /// </summary>
        [Operations(OperationKeyTypes.Search)]
        [Parameters("@LastLoginDateTimeTo", SqlDbType.DateTime)]
        public DateTime? LastLoginDateTimeTo { get; set; }

        /// <summary>
        /// Block status of the User
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@Blocked", SqlDbType.Bit)]
        public bool? Blocked { get; set; }

        /// <summary>
        /// Rights of the User
        /// </summary>
        public Right Rights { get; set; }
    }
}
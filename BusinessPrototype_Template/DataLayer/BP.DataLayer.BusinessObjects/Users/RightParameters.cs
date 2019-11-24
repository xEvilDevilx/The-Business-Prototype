using System;
using System.Data;

using BP.DataLayer.Base.Attributes;
using BP.DataLayer.Base.Enums;

namespace BP.DataLayer.BusinessObjects.Users
{
    /// <summary>
    /// Presents Right Parameters data object
    /// 
    /// 2018/11/12 - Created, IMasenko
    /// </summary>
    public class RightParameters
    {
        /// <summary>
        /// Right ID
        /// </summary>
        [Operations(
            OperationKeyTypes.Update |
            OperationKeyTypes.Get |
            OperationKeyTypes.Delete |
            OperationKeyTypes.Search)]
        [Parameters("@RightID", SqlDbType.UniqueIdentifier)]
        public Guid? RightID { get; set; }

        /// <summary>
        /// Right Name
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Search)]
        [Parameters("@RightName", SqlDbType.NVarChar)]
        public string RightName { get; set; }
        
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
        [Parameters("@RightNameTranslation", SqlDbType.NVarChar)]
        public string RightNameTranslation { get; set; }
    }
}

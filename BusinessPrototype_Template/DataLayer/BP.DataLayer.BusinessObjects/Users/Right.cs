using System;

namespace BP.DataLayer.BusinessObjects.Users
{
    /// <summary>
    /// Presents Right data object
    /// 
    /// 2018/11/12 - Modifided, IMasenko
    /// </summary>
    public class Right
    {
        /// <summary>
        /// User Right ID
        /// </summary>
        public Guid RightID { get; set; }

        /// <summary>
        /// Right Name
        /// </summary>
        public string RightName { get; set; }

        /// <summary>
        /// Right Name Translation
        /// </summary>
        public string RightNameTranslation { get; set; }
    }
}
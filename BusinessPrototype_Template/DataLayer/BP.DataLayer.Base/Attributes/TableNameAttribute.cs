using System;

namespace BP.DataLayer.Base.Attributes
{
    /// <summary>
    /// Presents Table Name Attribute for a data of the business object
    /// 
    /// 2018/11/23 - Created, VTyagunov
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TableNameAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Name of the Table in the database
        /// </summary>
        public string TableName { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tableName">Name of the Table in the database</param>
        public TableNameAttribute(string tableName)
        {
            TableName = tableName;
        }

        #endregion
    }
}
using BP.DataLayer.Databases.Base;
using BP.DataLayer.Databases.Base.Enums;

namespace BP.DataLayer.Databases.Interfaces
{
    /// <summary>
    /// Presents Base Database functionality interface for work with different databases (MSSQL, MySQL, etc)
    /// 
    /// 2018/10/08 - Created, VTyagunov
    /// </summary>
    public interface IBaseDatabase
    {
        /// <summary>
        /// Use for Check a Database Exists
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        /// <returns></returns>
        bool CheckDatabaseExists(ConnectionSettingsExt connectionSettings);

        /// <summary>
        /// Use for Create Database
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        void CreateDatabase(ConnectionSettingsExt connectionSettings);

        /// <summary>
        /// Use for Update Database
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        void UpdateDatabase(ConnectionSettingsExt connectionSettings);

        /// <summary>
        /// Check Database Version and return true if need Update the Database
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        bool IsNeedDatabaseUpdate(ConnectionSettingsExt connectionSettings);

        /// <summary>
        /// Use for Get current Database Version
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        /// <returns></returns>
        int GetDatabaseVersion(ConnectionSettingsExt connectionSettings);
    }
}
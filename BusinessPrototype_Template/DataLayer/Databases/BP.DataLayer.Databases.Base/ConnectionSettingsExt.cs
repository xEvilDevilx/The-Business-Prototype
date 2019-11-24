using BP.Database;
using BP.Database.Base;
using BP.DataLayer.Databases.Base.Enums;
using BP.SDK.Log;
using System;

namespace BP.DataLayer.Databases.Base
{
    /// <summary>
    /// Presents Connection Settings data
    /// 
    /// 2018/10/17 - Created, VTyagunov
    /// </summary>
    public class ConnectionSettingsExt : ConnectionSettings
    {
        /// <summary>
        /// Database Type
        /// </summary>
        public DatabaseTypes DatabaseType { get; set; }

        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnectionString
        {
            get
            {
                var connectionString = CreateConnectionString();
                return connectionString;
            }
        }

        /// <summary>
        /// Use for Create a Connection String
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString()
        {
            Logger.Log.Debug("ConnectionSettingsExt. CreateConnectionString");

            if (string.IsNullOrEmpty(SqlServerName))
                throw new ArgumentNullException(nameof(SqlServerName));

            if (string.IsNullOrEmpty(DatabaseName))
                throw new ArgumentNullException(nameof(DatabaseName));

            if (!WindowsAuth && string.IsNullOrEmpty(DatabaseUserName))
                throw new ArgumentNullException(nameof(DatabaseUserName),
                    @"In the Connection Settings set to use a SQL Server Authentication, therefore 
                    DatabaseUserName can not be null or empty!");

            if (!WindowsAuth && string.IsNullOrEmpty(DatabaseUserPassword))
                throw new ArgumentNullException(nameof(DatabaseUserPassword),
                    @"In the Connection Settings set to use a SQL Server Authentication, therefore 
                    DatabaseUserPassword can not be null or empty!");

            // Sql Server Name
            var sqlServerName = SqlServerName;

            // Database name
            string dbName = DatabaseName;

            // Windows Authantication
            bool windowsAuth = WindowsAuth;

            // Database User Name
            string dbUserName = string.Empty;
            if (!windowsAuth)
                dbUserName = DatabaseUserName;

            // Database User Password
            var dbUserPassword = string.Empty;
            if (!windowsAuth)
                dbUserPassword = DatabaseUserPassword;

            // Connection Type
            var connectionType = ConnectionType;

            // Connection String for Master database
            string connectionString = DatabaseUtils.CreateConnectionString(sqlServerName, dbName, windowsAuth,
                dbUserName, dbUserPassword, connectionType);

            Logger.Log.InfoFormat("ConnectionSettingsExt. CreateConnectionString. The Connection String is created: [{0}]",
                connectionString);
            return connectionString;
        }
    }
}
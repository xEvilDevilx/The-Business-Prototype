using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using BP.Database;
using BP.DataLayer.Databases.Base;
using BP.DataLayer.Databases.Base.Enums;
using BP.DataLayer.Databases.Interfaces;
using BP.SDK.Log;

namespace BP.DataLayer.Databases.MSSQL
{
    /// <summary>
    /// Presents Base Database functionality interface for work with different databases (MSSQL, MySQL, etc)
    /// 
    /// 2018/10/08 - Created, VTyagunov
    /// </summary>
    public class DatabaseAdministrator : IBaseDatabase
    {
        #region Variables

        /// <summary>SQL Scripts Manager</summary>
        private ISQLScriptsManager _sqlScriptsManager;

        /// <summary>Default name of the Database</summary>
        private const string _defaultDatabaseName = "BPDB";
        /// <summary>Name of the Version table</summary>
        private const string _versionTableName = "VERSION";
        /// <summary>Key of the Database Version</summary>
        private const string _dbVersionKey = "DBVersion";

        #region SQL

        /// <summary>SQL script for check database exists</summary>
        private const string _databaseExistsSQL = @"
            IF NOT EXISTS (SELECT [NAME] FROM SYS.DATABASES WHERE [NAME] = '{0}')            
            BEGIN
                SELECT 0 as 'RESULT';
            END
            ELSE
            BEGIN
                SELECT 1 as 'RESULT';
            END";

        /// <summary>Sql query for Create Database</summary>
        private const string _createDatabaseSQL = @"
            USE [MASTER];  
            GO  

            IF NOT EXISTS (SELECT [NAME] FROM SYS.DATABASES WHERE [NAME] = '{1}')            
            BEGIN
                DECLARE @DataDir NVARCHAR(256);
    	        SET @DataDir = {0};

                DECLARE @CreateDBSql NVARCHAR(max);
                SELECT @CreateDBSql = 
                'CREATE DATABASE {1}  
                ON   
                (NAME = {1}_dat,  
                    FILENAME = ''' + @DataDir + '{1}_dat.mdf'',  
                    SIZE = 200,  
                    MAXSIZE = UNLIMITED,  
                    FILEGROWTH = 20%)  
                LOG ON  
                (NAME = {1}_log,  
                    FILENAME = ''' + @DataDir + '{1}_log.ldf'',  
                    SIZE = 20MB,  
                    MAXSIZE = 200MB,  
                    FILEGROWTH = 5MB)';

				EXEC(@CreateDBSql);                
            END
            GO";

        /// <summary>Sql query presents Default Data Directory</summary>
        private const string _defaultDataDirSQL = @"
            (SELECT SUBSTRING(physical_name, 1, CHARINDEX(N'master.mdf', LOWER(physical_name)) - 1)
					FROM master.sys.master_files
					WHERE database_id = 1 AND file_id = 1)";

        /// <summary>Sql query for Create Version table</summary>
        private const string _createVersionTableSQL = @"
            USE {0};
            GO

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{1}')
            BEGIN
              CREATE TABLE [dbo].[{1}] (
                [KEY] [NVARCHAR](20) NOT NULL,
	            [VALUE] [NVARCHAR](10) NOT NULL DEFAULT (''),
                CONSTRAINT [PK_{1}] PRIMARY KEY CLUSTERED
                (
                    [KEY] ASC
                )
              )
            END
            GO";

        /// <summary>SQL query for Update Database Version</summary>
        private const string _updateDatabaseVersionSQL = @"
            USE {0};
            GO

            BEGIN
                UPDATE [dbo].[{1}] SET [VALUE] = {2} WHERE [KEY] = '{3}'
            END
            GO";

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseAdministrator()
        {
            Logger.Log.Debug("DatabaseAdministrator. Ctr");

            _sqlScriptsManager = new SQLScriptsManager();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use for Check a Database Exists
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        /// <returns></returns>
        public bool CheckDatabaseExists(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("DatabaseAdministrator. CheckDatabaseExists");

            if (connectionSettings == null)
                throw new ArgumentNullException(nameof(connectionSettings));

            try
            {
                // Database name
                string masterDBName = "master";
                string newDBName = string.Empty;
                if (string.IsNullOrEmpty(connectionSettings.DatabaseName))
                    newDBName = _defaultDatabaseName;
                else newDBName = connectionSettings.DatabaseName;

                string oldDatabaseName = connectionSettings.DatabaseName;
                connectionSettings.DatabaseName = masterDBName;
                string connectionString = CreateConnectionString(connectionSettings);                
                using (var databaseManager = new DatabaseManager(connectionString))
                {
                    var databaseExistsSQL = string.Format(_databaseExistsSQL, newDBName);
                    var dbExists = databaseManager.Execute<Boolean>(databaseExistsSQL, CommandType.Text, new SqlParameter[0],
                        DatabaseExistsPopulator);

                    connectionSettings.DatabaseName = oldDatabaseName;
                    return dbExists;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Create a Connection String
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        /// <returns></returns>
        private string CreateConnectionString(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("DatabaseAdministrator. CreateConnectionString");

            if (connectionSettings == null)
                throw new ArgumentNullException(nameof(connectionSettings));

            if (string.IsNullOrEmpty(connectionSettings.SqlServerName))
                throw new ArgumentNullException(nameof(connectionSettings.SqlServerName));

            if (!connectionSettings.WindowsAuth && string.IsNullOrEmpty(connectionSettings.DatabaseUserName))
                throw new ArgumentNullException(nameof(connectionSettings.DatabaseUserName),
                    @"For a Create Database it use a SQL Server Authentication, therefore 
                    DatabaseUserName can not be null or empty!");

            if (!connectionSettings.WindowsAuth && string.IsNullOrEmpty(connectionSettings.DatabaseUserPassword))
                throw new ArgumentNullException(nameof(connectionSettings.DatabaseUserPassword),
                    @"For a Create Database it use a SQL Server Authentication, therefore 
                    DatabaseUserPassword can not be null or empty!");

            // Data Directory
            string dataDir = string.Empty;
            if (string.IsNullOrEmpty(connectionSettings.DataDir))
                dataDir = _defaultDataDirSQL;
            else dataDir = connectionSettings.DataDir;

            // Sql Server Name
            var sqlServerName = connectionSettings.SqlServerName;

            // Database name
            string dbName = string.Empty;
            if (string.IsNullOrEmpty(connectionSettings.DatabaseName))
                dbName = _defaultDatabaseName;
            else dbName = connectionSettings.DatabaseName;

            // Windows Authantication
            bool windowsAuth = connectionSettings.WindowsAuth;

            // Database User Name
            string dbUserName = string.Empty;
            if (!windowsAuth)
                dbUserName = connectionSettings.DatabaseUserName;

            // Database User Password
            var dbUserPassword = string.Empty;
            if (!windowsAuth)
                dbUserPassword = connectionSettings.DatabaseUserPassword;

            // Connection Type
            var connectionType = connectionSettings.ConnectionType;

            //if (!Directory.Exists(dataDir))
            //    DirectoryAdvanced.CreateDirectoryFullAcess(dataDir);

            // Connection String for Master database
            string connectionString = DatabaseUtils.CreateConnectionString(sqlServerName, dbName, windowsAuth,
                dbUserName, dbUserPassword, connectionType);

            Logger.Log.InfoFormat("DatabaseAdministrator. CreateConnectionString. The Connection String is created: [{0}]",
                connectionString);
            return connectionString;
        }

        /// <summary>
        /// Use for Create Database
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        public void CreateDatabase(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("DatabaseAdministrator. CreateDatabase");

            if (connectionSettings == null)
                throw new ArgumentNullException(nameof(connectionSettings));

            // Data Directory
            string dataDir = string.Empty;
            if (string.IsNullOrEmpty(connectionSettings.DataDir))
                dataDir = _defaultDataDirSQL;
            else dataDir = connectionSettings.DataDir;

            // Database name
            string masterDBName = "master";
            string newDBName = string.Empty;
            if (string.IsNullOrEmpty(connectionSettings.DatabaseName))
                newDBName = _defaultDatabaseName;
            else newDBName = connectionSettings.DatabaseName;

            try
            {
                // Connection String for Master database
                connectionSettings.DatabaseName = masterDBName;
                string connectionString = CreateConnectionString(connectionSettings);                                               
                using (var databaseManager = new DatabaseManager(connectionString))
                {
                    var createDatabaseSQL = string.Format(_createDatabaseSQL, dataDir, newDBName);
                    databaseManager.ExecuteNonQuery(createDatabaseSQL, CommandType.Text, null);
                    bool dbExistsResult = databaseManager.DatabaseBase.CheckDBExists(newDBName);
                    if (!dbExistsResult)
                    {
                        var message = string.Format("Database with name '{0}' wasn't created! Please check connection settings.", newDBName);
                        throw new Exception(message);
                    }

                    Logger.Log.InfoFormat("DatabaseAdministrator. CreateDatabase. The Database with name '{0}' was created successful!", newDBName);                    
                }

                // Connection String for BP database
                connectionSettings.DatabaseName = newDBName;
                connectionString = CreateConnectionString(connectionSettings);                
                using (var databaseManager = new DatabaseManager(connectionString))
                {
                    var createVersionTableSQL = string.Format(_createVersionTableSQL, newDBName, _versionTableName);
                    databaseManager.ExecuteNonQuery(createVersionTableSQL, CommandType.Text, null);
                    bool tableExistsResult = databaseManager.DatabaseBase.CheckTableExists(_versionTableName);
                    if (!tableExistsResult)
                    {
                        var message = string.Format("Table with name '{0}' wasn't created in the {1} database! Please check connection settings.", 
                            _versionTableName, newDBName);
                        throw new Exception(message);
                    }

                    Logger.Log.InfoFormat("DatabaseAdministrator. CreateDatabase. The table with name '{0}' in the {1} database was created successful!",
                        _versionTableName, newDBName);

                    var addDefaultRow = string.Format("INSERT INTO [dbo].[{0}] ([KEY], [VALUE]) VALUES('{1}', '{2}')",
                        _versionTableName, _dbVersionKey, 0);
                    databaseManager.ExecuteNonQuery(addDefaultRow, CommandType.Text, null);

                    Logger.Log.Info("DatabaseAdministrator. CreateDatabase. Operation complete.");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Update Database
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        public void UpdateDatabase(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("DatabaseAdministrator. UpdateDatabase.");

            try
            {
                int currentDBVersion = GetDatabaseVersion(connectionSettings);

                string sqlScriptsFolderName = string.Empty;
                switch (connectionSettings.DatabaseType)
                {
                    case DatabaseTypes.Main:
                        sqlScriptsFolderName = "MainDBSQL";
                        break;
                    case DatabaseTypes.Point:
                        sqlScriptsFolderName = "PointDBSQL";
                        break;
                    case DatabaseTypes.WebSite:
                        sqlScriptsFolderName = "WebSiteDBSQL";
                        break;
                }

                List<string> scriptFullNames = _sqlScriptsManager.GetScriptNames(sqlScriptsFolderName);
                var connectionString = DatabaseUtils.CreateConnectionString(connectionSettings.SqlServerName,
                    connectionSettings.DatabaseName, connectionSettings.WindowsAuth, connectionSettings.DatabaseUserName,
                    connectionSettings.DatabaseUserPassword, connectionSettings.ConnectionType);

                var intVer = currentDBVersion + 1;
                var ver = intVer.ToString("D6");

                using (var databaseManager = new DatabaseManager(connectionString))
                {
                    foreach (var sql in scriptFullNames)
                    {
                        if (sql.Contains(ver))
                        {
                            var sqlScript = _sqlScriptsManager.GetScript(sql);
                            databaseManager.ExecuteNonQuery(sqlScript, CommandType.Text, null);

                            intVer++;
                            ver = intVer.ToString("D6");
                        }
                    }

                    intVer--;
                    if (intVer > currentDBVersion)
                    {
                        var sqlQuery = string.Format(_updateDatabaseVersionSQL, connectionSettings.DatabaseName,
                            _versionTableName, intVer, _dbVersionKey);
                        databaseManager.ExecuteNonQuery(sqlQuery, CommandType.Text, null);

                        Logger.Log.InfoFormat("DatabaseAdministrator. UpdateDatabase. The Database was updated from version '{0}' to '{1}'.",
                            currentDBVersion, intVer);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check Database Version and return true if need Update the Database
        /// </summary>
        /// <param name="connectionSettings">Connection Settings object</param>
        public bool IsNeedDatabaseUpdate(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("DatabaseAdministrator. IsNeedDatabaseUpdate.");

            try
            {
                string sqlScriptsFolderName = string.Empty;                
                switch(connectionSettings.DatabaseType)
                {
                    case DatabaseTypes.Main:
                        sqlScriptsFolderName = "MainDBSQL";
                        break;
                    case DatabaseTypes.Point:
                        sqlScriptsFolderName = "PointDBSQL";
                        break;
                    case DatabaseTypes.WebSite:
                        sqlScriptsFolderName = "WebSiteDBSQL";
                        break;
                }

                // Get Last Script Version
                List<string> dbScripts = _sqlScriptsManager.GetScriptNames(sqlScriptsFolderName);

                if (dbScripts.Count > 0)
                {
                    string lastSqlScriptVersionString = _sqlScriptsManager.GetCleanScriptName(
                        dbScripts[dbScripts.Count - 1], sqlScriptsFolderName);

                    if (string.IsNullOrEmpty(lastSqlScriptVersionString))
                        throw new FormatException(lastSqlScriptVersionString);

                    var lastSqlScriptVersion = int.Parse(lastSqlScriptVersionString);

                    // Get Current DB Version
                    int currentDBVersion = GetDatabaseVersion(connectionSettings);

                    // Check Is Need Update
                    bool isNeedUpdate = currentDBVersion < lastSqlScriptVersion;
                    return isNeedUpdate;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get current Database Version
        /// </summary>
        /// <returns></returns>
        public int GetDatabaseVersion(ConnectionSettingsExt connectionSettings)
        {
            Logger.Log.Debug("DatabaseAdministrator. GetDatabaseVersion.");

            var connectionString = DatabaseUtils.CreateConnectionString(connectionSettings.SqlServerName, 
                connectionSettings.DatabaseName, connectionSettings.WindowsAuth, connectionSettings.DatabaseUserName, 
                connectionSettings.DatabaseUserPassword, connectionSettings.ConnectionType);
            var sqlQuery = string.Format("SELECT [VALUE] FROM {0} WHERE [KEY] = '{1}'", 
                _versionTableName, _dbVersionKey);
            
            try
            {
                using (var databaseManager = new DatabaseManager(connectionString))
                {
                    var dbVersionStr = databaseManager.Execute<String>(sqlQuery, CommandType.Text, new SqlParameter[0],
                        VersionPopulator);
                    if (string.IsNullOrEmpty(dbVersionStr))
                        throw new Exception("Database version can not be empty!");

                    var result = int.Parse(dbVersionStr);
                    
                    Logger.Log.InfoFormat("DatabaseAdministrator. GetDatabaseVersion. Current Version of the {0} database is '{1}'.",
                        connectionSettings.DatabaseName, dbVersionStr);
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for populate Version field
        /// </summary>
        /// <param name="reader">SqlDataReader object</param>
        /// <returns></returns>
        private String VersionPopulator(SqlDataReader reader)
        {
            Logger.Log.Debug("DatabaseAdministrator. VersionPopulator.");

            var str = reader["VALUE"] as String;
            return str;
        }

        /// <summary>
        /// Use for populate Database Exists field
        /// </summary>
        /// <param name="reader">SqlDataReader object</param>
        /// <returns></returns>
        private Boolean DatabaseExistsPopulator(SqlDataReader reader)
        {
            Logger.Log.Debug("DatabaseAdministrator. DatabaseExistsPopulator.");

            var result = (int)reader["RESULT"];
            if (result == 1)
                return true;
            else return false;
        }

        #endregion
    }
}
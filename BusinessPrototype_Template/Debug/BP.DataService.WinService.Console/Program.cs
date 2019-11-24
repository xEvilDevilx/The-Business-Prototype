using System;

using BP.Database.Base.Enums;
using BP.DataLayer.Databases.Base;
using BP.DataLayer.Databases.Base.Constants;
using BP.DataLayer.Databases.Base.Enums;
using BP.DataLayer.Databases.Interfaces;
using BP.DataLayer.Databases.MSSQL;
using BP.DataService.WinService.Contracts;
using BP.SDK.Configs;
using BP.SDK.Interfaces.Configs;

namespace BP.DataService.WinService.DebugConsole
{
    /// <summary>
    /// Presents Main Entry Point to app
    /// 
    /// 2018/09/19 - Created, VTyagunov
    /// </summary>
    class Program
    {
        /// <summary>Data Service Contract</summary>
        private static DataServiceContract _dataServiceContract;
        /// <summary>Configurator functionality</summary>
        private static IConfigurator _configurator;
        /// <summary>Name of the Config File</summary>
        private static string _configFileName;
        /// <summary>Connection Settings</summary>
        private static ConnectionSettingsExt _connectionSettings;
        /// <summary>Database Administrator</summary>
        private static IBaseDatabase _databaseAdmin;

        /// <summary>
        /// Main Entry Point to app
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            _configFileName = "DataServiceConfig";

            try
            {
                _configurator = new Configurator(_configFileName);
                _connectionSettings = new ConnectionSettingsExt();
                string sqlServerName = _configurator.ReadValue(DatabaseConfigConstantsExt.DBSqlServerNameConfigTag);
                _connectionSettings.SqlServerName = string.IsNullOrEmpty(sqlServerName) ? "localhost" : sqlServerName;
                string dbName = _configurator.ReadValue(DatabaseConfigConstantsExt.DBNameConfigTag);
                _connectionSettings.DatabaseName = string.IsNullOrEmpty(dbName) ? "BPDB" : dbName;
                string winAuthStr = _configurator.ReadValue(DatabaseConfigConstantsExt.DBWindowsAuthConfigTag);
                _connectionSettings.WindowsAuth = string.IsNullOrEmpty(winAuthStr) ? true : winAuthStr != "true" ? false : true;
                _connectionSettings.DatabaseUserName = _configurator.ReadValue(DatabaseConfigConstantsExt.DBUserNameConfigTag);
                _connectionSettings.DatabaseUserPassword = _configurator.ReadValue(DatabaseConfigConstantsExt.DBUserPasswordConfigTag);
                string connTypeStr = _configurator.ReadValue(DatabaseConfigConstantsExt.DBConnectionTypeConfigTag);
                Enum.TryParse(connTypeStr, out ConnectionTypes connectionType);
                _connectionSettings.ConnectionType = connectionType;
                var dbTypeStr = _configurator.ReadValue(DatabaseConfigConstantsExt.DBTypeTag);
                dbTypeStr = "0";
                byte dbTypeID = byte.Parse(dbTypeStr);
                _connectionSettings.DatabaseType = (DatabaseTypes)dbTypeID;

                _databaseAdmin = new DatabaseAdministrator();
                var dbExists = _databaseAdmin.CheckDatabaseExists(_connectionSettings);
                if (!dbExists)
                {
                    _databaseAdmin.CreateDatabase(_connectionSettings);
                    _databaseAdmin.UpdateDatabase(_connectionSettings);
                }
                else if (_databaseAdmin.IsNeedDatabaseUpdate(_connectionSettings))
                    _databaseAdmin.UpdateDatabase(_connectionSettings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                return;
            }

            _dataServiceContract = new DataServiceContract();
            _dataServiceContract.Start(_connectionSettings);

            Console.WriteLine("###### Data Service started! ######");
            Console.WriteLine("For stop server enter 'Q' and press 'Enter'");
            string str = string.Empty;
            while (str != "Q")
                str = Console.ReadLine();

            _dataServiceContract.Stop();
        }
    }
}
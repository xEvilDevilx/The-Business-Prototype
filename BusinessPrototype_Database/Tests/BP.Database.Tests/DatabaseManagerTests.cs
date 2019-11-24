using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data;
using System.Data.SqlClient;
using System.IO;

using BP.Database.Base.Enums;
using BP.Database.Interfaces;

namespace BP.Database.Tests
{
    /// <summary>
    /// Presents Database Manager Tests functionality
    /// 
    /// ATTENTION!!!
    /// 
    /// For a correct working of tests is need to start Visual Studio with Administration permissions and 
    /// create in the database a User for Login.
    /// Name: 'BPUser', Password: '123456'
    /// 
    /// There are not tests for a ExecuteNonQuery method and ExecuteNonQueryTransaction method, because this methods were used in an Init and Cleanup methods
    /// 
    /// ATTENTION!!!
    /// 
    /// 2018/09/10 - Created, VTyagunov
    /// </summary>
    [TestClass]
    public class DatabaseManagerTests
    {
        #region Types

        /// <summary>
        /// Presents Items type for test
        /// 
        /// 2018/09/16 - Created, VTyagunov
        /// </summary>
        private class ItemTest
        {
            public string ID { get; set; }
            public string Text { get; set; }
            public int Number { get; set; }

            public override int GetHashCode()
            {
                return (ID.GetHashCode() + Text.GetHashCode() + Number.GetHashCode());
            }

            public override bool Equals(object obj)
            {
                return ((ID == ((ItemTest)obj).ID) && (Text == ((ItemTest)obj).Text) && (Number == ((ItemTest)obj).Number));
            }
        }

        #endregion

        #region Variables

        private IDatabaseManager _databaseManager;
        private string _dataDir;
        private string _tableName;
        private string[] _itemIDArray;
        private string[] _itemTextArray;
        private int[] _itemNumberArray;

        #region Connection Settings

        private string _sqlServerName;
        private string _dbName;
        private bool _windowsAuth;
        private string _dbUserName;
        private string _dbUserPassword;
        private ConnectionTypes _connectionType;
        private string _connectionString;

        #endregion

        #region SQL Scripts

        private string _createDatabaseSQL = @"
            USE master;  
            GO  

            IF EXISTS (SELECT [Name] FROM sys.databases WHERE [name] = '{0}')
            BEGIN
                DROP DATABASE {0}
            END
            GO

            BEGIN
                CREATE DATABASE {0}  
                ON   
                (NAME = {0}_dat,  
                    FILENAME = '{1}{0}_dat.mdf',  
                    SIZE = 20,  
                    MAXSIZE = 200,  
                    FILEGROWTH = 5)  
                LOG ON  
                (NAME = {0}_log,  
                    FILENAME = '{1}{0}_log.ldf',  
                    SIZE = 10MB,  
                    MAXSIZE = 100MB,  
                    FILEGROWTH = 5MB);
            END
            GO";
        private string _dropDatabaseSQL = @"
            USE master;  
            GO  

            IF EXISTS (SELECT [Name] FROM sys.databases WHERE [name] = '{0}')
            BEGIN
                DROP DATABASE {0}
            END
            GO";
        private string _createTableSQL = @"
            USE {0};
            GO

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = 'dbo' AND table_name = '{1}')
            BEGIN
              CREATE TABLE [dbo].[{1}] (
                [ID]	     NVARCHAR(20) NOT NULL
               ,[TEXT]       NVARCHAR(60) NOT NULL CONSTRAINT [{1}_DF_TEXT]  DEFAULT ('')
               ,[NUMBER]     INT NOT NULL
               ,CONSTRAINT [PK_{1}] PRIMARY KEY CLUSTERED
                (
                    [ID] ASC, 
                    [TEXT] ASC
                )
              )
            END
            GO";
        private string _addRowSQL = @"
USE {0};
GO

BEGIN
IF NOT EXISTS (SELECT * FROM {1} WHERE ID = '{2}')
   BEGIN
	   INSERT INTO {1} (ID, TEXT, NUMBER)
	   VALUES ('{2}', '{3}', {4})
   END
END
GO";

        #endregion

        #endregion

        #region Methods

        [TestInitialize]
        public void Init()
        {
            // Prepare [Create DB]
            _dataDir = @"G:\DEVELOPMENT\BusinessPrototype\Tests\DATA\";
            _sqlServerName = "ETGD";
            _dbName = "BP_Database_Tests";
            _windowsAuth = false;
            _dbUserName = "BPUser";
            _dbUserPassword = "123456";
            _connectionType = ConnectionTypes.TCPIP;
            _connectionString = string.Empty;

            CreateDataCatalog();

            _createDatabaseSQL = string.Format(_createDatabaseSQL, _dbName, _dataDir);
            _connectionString = DatabaseUtils.CreateConnectionString(_sqlServerName, "master", _windowsAuth,
                _dbUserName, _dbUserPassword, _connectionType);
            _databaseManager = new DatabaseManager(_connectionString);

            // Execution [Create DB]
            _databaseManager.ExecuteNonQuery(_createDatabaseSQL, CommandType.Text, null);
            bool dbExistsResult = _databaseManager.DatabaseBase.CheckDBExists(_dbName);

            // Check [Create DB]
            Assert.IsTrue(dbExistsResult);

            // Prepare [Create Table]
            _tableName = "BPTESTTABLE";
            _databaseManager.Dispose();
            _connectionString = DatabaseUtils.CreateConnectionString(_sqlServerName, _dbName, _windowsAuth,
                _dbUserName, _dbUserPassword, _connectionType);
            _databaseManager = new DatabaseManager(_connectionString);
            _createTableSQL = string.Format(_createTableSQL, _dbName, _tableName);

            // Execution [Create Table]
            _databaseManager.ExecuteNonQueryTransaction(_createTableSQL, CommandType.Text, null);
            bool tableExistsResult = _databaseManager.DatabaseBase.CheckTableExists(_tableName);

            // Check [Create Table]
            Assert.IsTrue(tableExistsResult);

            // Prepare [Add rows]
            _itemIDArray = new string[]
            {
                "12x5",
                "z13p4",
                "6aa5s"
            };
            _itemTextArray = new string[]
            {
                "First",
                "Second",
                "Third"
            };
            _itemNumberArray = new int[]
            {
                111,
                222,
                333
            };

            // Execution [Add rows]
            for (int i = 0; i < 3; i++)
            {
                var iStr = string.Format("i[{0}]", i);
                var addRowSQL = string.Format(_addRowSQL, _dbName, _tableName, _itemIDArray[i], _itemTextArray[i], _itemNumberArray[i]);
                _databaseManager.ExecuteNonQuery(addRowSQL, CommandType.Text, null);
            }

            // Check [Add rows]
        }

        [TestCleanup]
        public void CleanUp()
        {
            // Prepare
            _dropDatabaseSQL = string.Format(_dropDatabaseSQL, _dbName);

            // Execution
            _databaseManager.ExecuteNonQuery(_dropDatabaseSQL, CommandType.Text, null);
            bool notExistsResult = _databaseManager.DatabaseBase.CheckDBExists(_dbName);

            // Checking
            Assert.IsFalse(notExistsResult);
        }

        private void CreateDataCatalog()
        {
            var dirExists = Directory.Exists(_dataDir);
            if (!dirExists)
                Directory.CreateDirectory(_dataDir);
        }

        private ItemTest ItemPopulator(SqlDataReader reader)
        {
            var item = new ItemTest()
            {
                ID = reader["ID"] as string,
                Text = reader["TEXT"] as string,
                Number = (int)reader["NUMBER"]
            };
            return item;
        }

        [TestMethod]
        [Priority(0)]
        public void ExecuteTest()
        {
            // Prepare
            var sqlQuery = string.Format("SELECT [ID], [TEXT], [NUMBER] FROM {0} WHERE [ID] = '{1}'", _tableName, _itemIDArray[1]);

            // Execution
            var item = _databaseManager.Execute<ItemTest>(sqlQuery, System.Data.CommandType.Text, new SqlParameter[0], ItemPopulator);

            // Checking
            Assert.IsTrue(item.ID == _itemIDArray[1]);
            Assert.IsTrue(item.Text == _itemTextArray[1]);
            Assert.IsTrue(item.Number == _itemNumberArray[1]);
        }

        [TestMethod]
        [Priority(1)]
        public void ExecuteListTest()
        {
            // Prepare
            var sqlQuery = string.Format("SELECT [ID], [TEXT], [NUMBER] FROM {0}", _tableName);

            // Execution
            var items = _databaseManager.ExecuteList<ItemTest>(sqlQuery, System.Data.CommandType.Text, new SqlParameter[0], ItemPopulator);

            // Checking
            var firstItem = new ItemTest() { ID = _itemIDArray[0], Text = _itemTextArray[0], Number = _itemNumberArray[0] };
            var secondItem = new ItemTest() { ID = _itemIDArray[1], Text = _itemTextArray[1], Number = _itemNumberArray[1] };
            var thirdItem = new ItemTest() { ID = _itemIDArray[2], Text = _itemTextArray[2], Number = _itemNumberArray[2] };

            var isContains = items.Contains(firstItem);
            Assert.IsTrue(isContains);

            isContains = items.Contains(secondItem);
            Assert.IsTrue(isContains);

            isContains = items.Contains(thirdItem);
            Assert.IsTrue(isContains);
        }

        [TestMethod]
        [Priority(2)]
        public void ExecuteTransactionTest()
        {
            // Prepare
            var sqlQuery = string.Format("SELECT [ID], [TEXT], [NUMBER] FROM {0} WHERE [ID] = '{1}'", _tableName, _itemIDArray[1]);

            // Execution
            var item = _databaseManager.ExecuteTransaction<ItemTest>(sqlQuery, System.Data.CommandType.Text, new SqlParameter[0], ItemPopulator);

            // Checking
            Assert.IsTrue(item.ID == _itemIDArray[1]);
            Assert.IsTrue(item.Text == _itemTextArray[1]);
            Assert.IsTrue(item.Number == _itemNumberArray[1]);
        }

        [TestMethod]
        [Priority(3)]
        public void ExecuteListTransactionTest()
        {
            // Prepare
            var sqlQuery = string.Format("SELECT [ID], [TEXT], [NUMBER] FROM {0}", _tableName);

            // Execution
            var items = _databaseManager.ExecuteListTransaction<ItemTest>(
                sqlQuery, System.Data.CommandType.Text, new SqlParameter[0], ItemPopulator);

            // Checking
            var firstItem = new ItemTest() { ID = _itemIDArray[0], Text = _itemTextArray[0], Number = _itemNumberArray[0] };
            var secondItem = new ItemTest() { ID = _itemIDArray[1], Text = _itemTextArray[1], Number = _itemNumberArray[1] };
            var thirdItem = new ItemTest() { ID = _itemIDArray[2], Text = _itemTextArray[2], Number = _itemNumberArray[2] };

            var isContains = items.Contains(firstItem);
            Assert.IsTrue(isContains);

            isContains = items.Contains(secondItem);
            Assert.IsTrue(isContains);

            isContains = items.Contains(thirdItem);
            Assert.IsTrue(isContains);
        }

        #endregion
    }
}
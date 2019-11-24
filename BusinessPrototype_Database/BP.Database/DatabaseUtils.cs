using System;

using BP.Database.Base.Enums;
using BP.Database.Properties;

namespace BP.Database
{
    /// <summary>
    /// Implements Database Util functionality
    /// 
    /// 2018/09/10 - Created, VTyagunov
    /// </summary>
    public class DatabaseUtils
    {
        /// <summary>
        /// Use for Create Connection String
        /// </summary>
        /// <param name="sqlServerName">The SQL server name</param>
        /// <param name="dbName">The database name</param>
        /// <param name="windowsAuth">The Windows Authentication flag</param>
        /// <param name="dbUserName">The database user name for the Sql Server auth</param>
        /// <param name="dbUserPassword">The database user password for the Sql Server auth</param>
        /// <param name="connectionType">The database connection type</param>
        public static string CreateConnectionString(string sqlServerName, string dbName, bool windowsAuth, 
            string dbUserName, string dbUserPassword, ConnectionTypes connectionType)
        {
            if (string.IsNullOrEmpty(sqlServerName) || string.IsNullOrEmpty(dbName))
                throw new Exception(Resource.CannotCreateConnectionString);

            string connectionString = @"Data Source=" + sqlServerName + ";Initial Catalog=" + dbName;
            if (windowsAuth)
                connectionString += ";Integrated Security=True;";
            else connectionString += ";Integrated Security=False;UID=" + dbUserName + ";PWD=" + dbUserPassword + ";";

            switch (connectionType)
            {
                case ConnectionTypes.TCPIP:
                    connectionString += "Network Library=DBMSSOCN;";
                    break;                
                case ConnectionTypes.NamedPipes:
                    connectionString += "Network Library=DBNMPNTW;";
                    break;
                case ConnectionTypes.SharedMemory:
                    connectionString += "Network Library=DBMSLPCN;";
                    break;
            }

            connectionString += "Max Pool Size=500;";

            return connectionString;
        }

        /// <summary>
        /// Use for Create Temp Connection String
        /// </summary>
        /// <param name="sqlServerName">The SQL server name</param>
        public static string CreateTempConnectionString(string sqlServerName)
        {
            if (string.IsNullOrEmpty(sqlServerName))
                throw new Exception(Resource.CannotCreateConnectionString);

            string tempConnectionString = string.Format("server={0};Trusted_Connection=yes", sqlServerName);
            return tempConnectionString;
        }
    }
}
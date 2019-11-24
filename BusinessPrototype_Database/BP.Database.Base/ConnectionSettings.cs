using BP.Database.Base.Enums;

namespace BP.Database.Base
{
    /// <summary>
    /// Presents Connection Settings data
    /// 
    /// 2018/10/14 - Created, VTyagunov
    /// </summary>
    public class ConnectionSettings
    {
        /// <summary>
        /// Directory of the databases data
        /// </summary>
        public string DataDir { get; set; }
        /// <summary>
        /// Name of the Sql Server
        /// </summary>
        public string SqlServerName { get; set; }
        /// <summary>
        /// Name of the Database
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary>
        /// Flag shows is need to use Windows Authentication or not
        /// </summary>
        public bool WindowsAuth { get; set; }
        /// <summary>
        /// User name of the Database Login data
        /// </summary>
        public string DatabaseUserName { get; set; }
        /// <summary>
        /// Password of the Database Login data
        /// </summary>
        public string DatabaseUserPassword { get; set; }
        /// <summary>
        /// Type of the Connection
        /// </summary>
        public ConnectionTypes ConnectionType { get; set; }
    }
}
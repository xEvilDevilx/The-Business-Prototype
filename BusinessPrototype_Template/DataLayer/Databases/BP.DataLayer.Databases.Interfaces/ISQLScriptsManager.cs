using System.Collections.Generic;

namespace BP.DataLayer.Databases.Interfaces
{
    /// <summary>
    /// Presents SQL Scripts Manager functionality interface
    /// 
    /// 2018/10/16 - Created, VTyagunov
    /// </summary>
    public interface ISQLScriptsManager
    {
        /// <summary>
        /// Use for Get Clean script name from Full name
        /// </summary>
        /// <param name="fullScriptName">Full script name</param>
        /// <param name="databaseTypeName">Name of the Database Type</param>
        /// <returns></returns>
        string GetCleanScriptName(string fullScriptName, string databaseTypeName);

        /// <summary>
        /// Use for Get Clean script name from Full name
        /// </summary>
        /// <param name="fullScriptNames">Full script names</param>
        /// <param name="databaseTypeName">Name of the Database Type</param>
        /// <returns></returns>
        List<int> GetCleanScriptVers(string[] fullScriptNames, string databaseTypeName);

        /// <summary>
        /// Use for Get script names array
        /// </summary>
        /// <param name="databaseTypeName">Name of the Database Type</param>
        /// <returns></returns>
        List<string> GetScriptNames(string databaseTypeName);

        /// <summary>
        /// Use for Get script by script id
        /// </summary>
        /// <param name="scriptID">Script ID</param>
        /// <returns></returns>
        string GetScript(string scriptID);
    }
}
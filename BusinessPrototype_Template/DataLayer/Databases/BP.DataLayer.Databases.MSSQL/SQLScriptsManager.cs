using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using BP.DataLayer.Databases.Interfaces;

namespace BP.DataLayer.Databases.MSSQL
{
    /// <summary>
    /// Implements SQL Scripts Manager functionality
    /// 
    /// 2018/10/16 - Created, VTyagunov
    /// </summary>
    public class SQLScriptsManager : ISQLScriptsManager
    {
        /// <summary>
        /// Use for Get Clean script name from Full name
        /// </summary>
        /// <param name="fullScriptName">Full script name</param>
        /// <param name="databaseTypeName">Name of the Database Type</param>
        /// <returns></returns>
        public string GetCleanScriptName(string fullScriptName, string databaseTypeName)
        {
            string result = string.Empty;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var assemblyName = assembly.GetName().Name;
                if (!fullScriptName.Contains(assemblyName) ||
                    !fullScriptName.Contains(databaseTypeName) ||
                    !fullScriptName.Contains(InternalConstants.PostfixExtensionName))
                    return fullScriptName;

                result = fullScriptName.Replace(assemblyName + ".", string.Empty).
                    Replace(databaseTypeName + ".", string.Empty).
                    Replace(InternalConstants.PostfixExtensionName, string.Empty);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get Clean script name from Full name
        /// </summary>
        /// <param name="fullScriptNames">Full script names</param>
        /// <param name="databaseTypeName">Name of the Database Type</param>
        /// <returns></returns>
        public List<int> GetCleanScriptVers(string[] fullScriptNames, string databaseTypeName)
        {
            var result = new List<int>();

            try
            {
                foreach (var fullScriptName in fullScriptNames)
                {
                    var cleanScriptName = GetCleanScriptName(fullScriptName, databaseTypeName);
                    var scriptVer = int.Parse(cleanScriptName);
                    result.Add(scriptVer);
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get script names array
        /// </summary>
        /// <param name="databaseTypeName">Name of the Database Type</param>
        /// <returns></returns>
        public List<string> GetScriptNames(string databaseTypeName)
        {
            try
            {
                var dbScripts = new List<string>();
                Assembly assembly = Assembly.GetExecutingAssembly();
                string[] sqlScriptFullNames = assembly.GetManifestResourceNames();

                for (int i = 0; i < sqlScriptFullNames.Length; i++)
                {
                    if (sqlScriptFullNames[i].Contains(databaseTypeName))
                        dbScripts.Add(sqlScriptFullNames[i]);
                }

                return dbScripts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Get script by script id
        /// </summary>
        /// <param name="scriptID">Script ID</param>
        /// <returns></returns>
        public string GetScript(string scriptID)
        {
            string result = "";

            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                using (var stream = assembly.GetManifestResourceStream(scriptID))
                using (var reader = new StreamReader(stream))
                    result = reader.ReadToEnd();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
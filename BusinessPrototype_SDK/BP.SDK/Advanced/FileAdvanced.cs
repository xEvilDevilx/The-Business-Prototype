using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

using BP.SDK.Log;

namespace BP.SDK.Advanced
{
    /// <summary>
    /// Implements File Advanced functionality
    /// 
    /// 2018/10/10 - Created, VTyagunov
    /// </summary>
    public static class FileAdvanced
    {
        /// <summary>
        /// Use for Create File with Full Access
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <returns></returns>
        public static FileStream CreateFileFullAcess(string path)
        {
            Logger.Log.Debug("FileAdvanced. CreateFileFullAcess");

            var securityRules = new FileSecurity();
            var everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            securityRules.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.FullControl, AccessControlType.Allow));

            FileStream fs = File.Create(path, 1024, FileOptions.Asynchronous, securityRules);
            return fs;
        }
    }
}
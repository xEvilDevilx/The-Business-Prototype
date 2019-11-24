using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

using BP.SDK.Log;

namespace BP.SDK.Advanced
{
    /// <summary>
    /// Implements Directory Advanced functionality
    /// 
    /// 2018/10/10 - Created, VTyagunov
    /// </summary>
    public static class DirectoryAdvanced
    {
        /// <summary>
        /// Use for Create Directory with Full Access
        /// </summary>
        /// <param name="path">Path of the directory</param>
        /// <returns></returns>
        public static DirectoryInfo CreateDirectoryFullAcess(string path)
        {
            Logger.Log.Debug("DirectoryAdvanced. CreateDirectoryFullAcess");

            var securityRules = new DirectorySecurity();
            var everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            securityRules.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.FullControl, AccessControlType.Allow));

            DirectoryInfo di = Directory.CreateDirectory(path, securityRules);
            return di;
        }
    }
}
using System;
using System.Security;

using BP.SDK.Log;

namespace BP.SDK.Extensions
{
    /// <summary>
    /// Implements String Extensions functionality
    /// 
    /// 2018/10/06 - Created, VTyagunov
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Use for Convert to a SecureString
        /// </summary>
        /// <param name="password">Password string</param>
        /// <returns></returns>
        public static SecureString ConvertToSecureString(this string password)
        {
            Logger.Log.Debug("StringExtensions. LoadAssemblies");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            var securePassword = new SecureString();

            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }
    }
}